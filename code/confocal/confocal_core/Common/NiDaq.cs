using confocal_core.Model;
using confocal_core.ViewModel;
using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    /// <summary>
    /// 模拟输入采集事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="samples"></param>
    public delegate void AiSamplesReceivedEventHandler(object sender, short[][] samples);
    /// <summary>
    /// 计数器计数事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="channelIndex"></param>
    /// <param name="samples"></param>
    public delegate void CiSamplesReceivedEventHandler(object sender, int channelIndex, int[] samples);

    /// <summary>
    /// NI板卡接口类
    /// </summary>
    public class NiDaq
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public AiSamplesReceivedEventHandler AiSamplesReceived;
        public CiSamplesReceivedEventHandler CiSamplesReceived;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private SchedulerViewModel mConfig;
        private Task mAoTask;
        private Task mDoTask;
        private Task mAiTask;
        private Task[] mCiTasks;
        private AnalogUnscaledReader mAiUnscaledReader;
        private CounterSingleChannelReader[] mCiChannelReaders;
        private int[] mAiChannelIndex;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public NiDaq(SchedulerViewModel config)
        {
            mConfig = config;
            mAoTask = null;
            mDoTask = null;
            mAiTask = null;
            mCiTasks = null;
            mAiUnscaledReader = null;
            mCiChannelReaders = null;

            int channelNum = mConfig.GetChannelNum();
            mAiChannelIndex = Enumerable.Repeat<int>(-1, channelNum).ToArray();
        }




        /// <summary>
        /// 配置AI任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAiTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            try
            {
                mAiTask = new Task();

                mAiTask.AIChannels.CreateVoltageChannel(GetAiPhysicalChannelName(), "", AITerminalConfiguration.Differential, -5.0, 5.0, AIVoltageUnits.Volts);

                mAiTask.Control(TaskAction.Verify);

                mAiTask.Timing.SampleClockRate = Z1Sequence.InputSampleRate;
                mAiTask.Timing.ConfigureSampleClock("",
                    mAiTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    Z1Sequence.InputSampleCountPerFrame);

                // 设置Ai Start Trigger源为PFIx，PFIx与Acq Trigger[一般是Do]物理直连，接收Do的输出信号，作为触发
                string source = mConfig.Detector.TriggerSignal;
                mAiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);
                mAiTask.Triggers.StartTrigger.Retriggerable = true;        // 设置为允许重触发

                // mAiTask.AIChannels.All.DataTransferMechanism = AIDataTransferMechanism.Dma;

                // 路由AI Sample Clcok到PFI2， AI Convert Clock到PFI3
                //if (m_config.Debugging)
                //{
                //    Logger.Info(string.Format("route ai sample clock to FFI2, ai convert clock to PFI3."));
                //    mAiTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI2"); ;
                //    mAiTask.ExportSignals.AIConvertClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI3"); ;
                //}

                mAiTask.EveryNSamplesReadEventInterval = Z1Sequence.InputSampleCountPerAcquisition;
                // mAiTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(AiEveryNSamplesRead);

                mAiUnscaledReader = new AnalogUnscaledReader(mAiTask.Stream);
                mAiUnscaledReader.SynchronizeCallbacks = false;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ai task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_AI_TASK_EXCEPTION;
            }

            return code;
        }

        /// <summary>
        /// 获取AO物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetAoPhysicalChannelName()
        {
            string physicalChannelName = string.Empty;
            if (mConfig.SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
            {
                physicalChannelName = string.Concat(mConfig.GalvoProperty.XGalvoAoChannel, ",", mConfig.GalvoProperty.YGalvoAoChannel, ",", mConfig.GalvoProperty.Y2GalvoAoChannel);
            }
            else
            {
                physicalChannelName = string.Concat(mConfig.GalvoProperty.XGalvoAoChannel, ",", mConfig.GalvoProperty.YGalvoAoChannel);
            }
            Logger.Info(string.Format("ao physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        /// <summary>
        /// 获取DO物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetDoPhysicalChannelName()
        {
            return mConfig.Detector.TriggerSignal;
        }

        /// <summary>
        /// 获取CI物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetCiSampleClockSourceName()
        {
            string aoDeviceName = mConfig.GalvoProperty.XGalvoAoChannel.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
            return string.Concat("/", aoDeviceName, "/ao/SampleClock");
        }

        /// <summary>
        /// 获取AI物理通道名
        /// </summary>
        /// <returns></returns>
        private string GetAiPhysicalChannelName()
        {
            List<string> activatedChannelNames = new List<string>();
            int channelNum = mConfig.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                if (mConfig.FindScanChannel(i).Activated)
                {
                    activatedChannelNames.Add(mConfig.Detector.FindPmtChannel(i).AiChannel);
                }
            }
            string physicalChannelName = string.Join(",", activatedChannelNames.ToArray());
            Logger.Info(string.Format("ai physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        /// <summary>
        /// 使用PMT时，生成激活的通道对应的模拟通道序列号
        /// </summary>
        public void GenerateAiChannelIndex()
        {
            int index = -1;
            int channelNum = mConfig.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                mAiChannelIndex[i] = mConfig.FindScanChannel(i).Activated ? ++index : -1;
            }
        }

        public int GetLaserAiChannelIndex(int channelId)
        {
            return mAiChannelIndex[channelId];
        }

        private int FindCiMultiChannelReaderIndex(object sender)
        {
            for (int i = 0; i < mCiTasks.Length; i++)
            {
                if (mCiTasks[i] != null && mCiTasks[i].Equals(sender))
                {
                    return i;
                }
            }
            return -1;
        }



        public static string[] GetDeviceNames()
        {
            return DaqSystem.Local.Devices;
        }

        public static string GetDeviceType(string device)
        {
            return DaqSystem.Local.LoadDevice(device).ProductType;
        }

        public static string[] GetAoChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AO, PhysicalChannelAccess.External);
        }

        public static string[] GetAiChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External);
        }

        public static string[] GetDoLines()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DOLine, PhysicalChannelAccess.External);
        }

        public static string[] GetCiChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.CI, PhysicalChannelAccess.External);
        }

        public static string[] GetPFIs()
        {
            string[] terminals = DaqSystem.Local.GetTerminals(TerminalTypes.Basic);
            List<string> pfis = new List<string>();

            foreach (string terminal in terminals)
            {
                if (terminal.Contains("/PFI"))
                {
                    pfis.Add(terminal);
                }
            }
            return pfis.ToArray();
        }

        public static string[] GetStartSyncSignals()
        {
            string[] terminals = DaqSystem.Local.GetTerminals(TerminalTypes.Basic);
            List<string> pfis = new List<string>();

            foreach (string terminal in terminals)
            {
                if (terminal.Contains("/StartTrigger"))
                {
                    pfis.Add(terminal);
                }
            }
            return pfis.ToArray();
        }

    }
}
