﻿using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public delegate void AiSamplesReceivedEventHandler(object sender, short[][] samples);
    public delegate void CiSamplesReceivedEventHandler(object sender, int channelIndex, int[] samples);
    /// <summary>
    /// NIDAQ采集卡接口层
    /// </summary>
    public class Z1NiDaq
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public AiSamplesReceivedEventHandler AiSamplesReceived;
        public CiSamplesReceivedEventHandler CiSamplesReceived;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Z1Config mConfig;
        private Z1Settings mSettings;
        private Task mAoTask;
        private Task mDoTask;
        private Task mAiTask;
        private Task[] mCiTasks;
        private AnalogUnscaledReader mAiUnscaledReader;
        private CounterSingleChannelReader[] mCiChannelReaders;
        private int[] mAiChannelIndex;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public Z1NiDaq()
        {
            mConfig = Z1Config.GetConfig();
            mSettings = Z1Settings.GetSettings();

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
        /// 启动任务
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE Start()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            code = ConfigAoTask();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            code = ConfigDoTask();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            if (mSettings.GetAcqDevice() == ACQ_DEVICE.PMT)
            {
                code = ConfigAiTask();
            }
            else
            {
                code = ConfigCiTasks();
            }
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            code = StartAllTasks();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            return code;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            if (mAoTask != null)
            {
                try
                {
                    mAoTask.Stop();
                    mAoTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop ao task exception: [{0}].", e));
                }
                mAoTask = null;
            }

            if (mDoTask != null)
            {
                try
                {
                    mDoTask.Stop();
                    mDoTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop do task exception: [{0}].", e));
                }
                mDoTask = null;
            }

            if (mAiTask != null)
            {
                try
                {
                    mAiTask.Stop();
                    mAiTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop ai task exception: [{0}].", e));
                }
                mAiTask = null;
                mAiUnscaledReader = null;
            }

            if (mCiTasks != null)
            {
                for (int i = 0; i < mCiTasks.Length; i++)
                {
                    try
                    {
                        if (mCiTasks[i] != null)
                        {
                            mCiTasks[i].Stop();
                            mCiTasks[i].Dispose();
                            mCiTasks[i] = null;
                        }
                    }
                    catch (DaqException e)
                    {
                        Logger.Error(string.Format("stop ci task[{0}] exception: [{1}].", i, e));
                    }
                }

                for (int i = 0; i < mCiChannelReaders.Length; i++)
                {
                    mCiChannelReaders[i] = null;
                }

                mCiTasks = null;
                mCiChannelReaders = null;
            }

        }

        /// <summary>
        /// 启动所有任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE StartAllTasks()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                if (mSettings.GetAcqDevice() == ACQ_DEVICE.PMT)
                {
                    mAiTask.Start();
                }
                else
                {
                    for (int i = 0; i < mCiTasks.Length; i++)
                    {
                        if (mCiTasks[i] != null)
                        {
                            mCiTasks[i].Start();
                        }
                    }
                }

                mDoTask.Start();
                mAoTask.Start();
            }
            catch (DaqException e)
            {
                Logger.Error(string.Format("start ni tasks exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_START_TASK_EXCEPTION;
            }

            return code;
        }

        /// <summary>
        /// 配置AO任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAoTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                mAoTask = new Task();

                mAoTask.AOChannels.CreateVoltageChannel(GetAoPhysicalChannelName(), "", -10.0, 10.0, AOVoltageUnits.Volts);
                mAoTask.Control(TaskAction.Verify);

                mAoTask.Timing.SampleClockRate = Z1Sequence.OutputSampleRate;
                mAoTask.Timing.ConfigureSampleClock("",
                    mAoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    Z1Sequence.OutputSampleCountPerFrame);

                //string source = mSettings.GetStartSyncSignal();
                //m_aoTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                // 写入波形
                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(mAoTask.Stream);
                AnalogWaveform<double>[] waves;
                if (mConfig.ScanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
                {
                    waves = new AnalogWaveform<double>[3];
                    waves[2] = AnalogWaveform<double>.FromArray1D(Z1Sequence.Y2Wave);
                }
                else
                {
                    waves = new AnalogWaveform<double>[2];
                }
                waves[0] = AnalogWaveform<double>.FromArray1D(Z1Sequence.XWave);
                waves[1] = AnalogWaveform<double>.FromArray1D(Z1Sequence.Y1Wave);
                writer.WriteWaveform(false, waves);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ao task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_AO_TASK_EXCEPTION;
            }
            return code;
        }

        /// <summary>
        /// 配置Do任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigDoTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                mDoTask = new Task();

                mDoTask.DOChannels.CreateChannel(GetDoPhysicalChannelName(), "", ChannelLineGrouping.OneChannelForEachLine);
                mDoTask.Control(TaskAction.Verify);

                mDoTask.Timing.SampleClockRate = Z1Sequence.OutputSampleRate;
                mDoTask.Timing.ConfigureSampleClock("",
                    mDoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    Z1Sequence.OutputSampleCountPerFrame);

                // 设置Do Start Trigger源为Ao Start Trigger[默认]，实现启动同步
                string source = mSettings.GetStartSyncSignal();
                mDoTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                mDoTask.Stream.WriteRegenerationMode = WriteRegenerationMode.AllowRegeneration;

                DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(mDoTask.Stream);
                DigitalWaveform wave = DigitalWaveform.FromPort(Z1Sequence.TriggerWave, 0x01);
                writer.WriteWaveform(false, wave);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config do task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_DO_TASK_EXCEPTION;
            }
            return code;
        }

        /// <summary>
        /// 配置CI任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigCiTasks()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            int channelNum = mConfig.GetChannelNum();
            mCiTasks = new Task[channelNum];
            mCiChannelReaders = new CounterSingleChannelReader[channelNum];
            for (int i = 0; i < channelNum; i++)
            {
                CHAN_ID id = (CHAN_ID)i;
                if (mConfig.ScanProperty.ScanChannels[i].ChannelSwitch == CHAN_SWITCH.ON)
                {
                    code = ConfigCiTask(mSettings.GetApdCiChannel(id), mSettings.GetApdCiSrcPfi(id), mSettings.GetApdTriggerInPfi(), ref mCiTasks[i], ref mCiChannelReaders[i]);
                    if (code != API_RETURN_CODE.API_SUCCESS)
                    {
                        return code;
                    }
                }
                else
                {
                    mCiTasks[i] = null;
                    mCiChannelReaders[i] = null;
                }
            }
            return code;
        }

        /// <summary>
        /// 配置CI任务
        /// </summary>
        /// <param name="ciChannel"></param>
        /// <param name="ciSrc"></param>
        /// <param name="ciClock"></param>
        /// <param name="ciTask"></param>
        /// <param name="ciMultiChannelReader"></param>
        /// <returns></returns>
        private API_RETURN_CODE ConfigCiTask(string ciChannel, string ciSrc, string ciClock, ref Task ciTask, ref CounterSingleChannelReader ciMultiChannelReader)
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            try
            {
                ciTask = new Task();

                ciTask.CIChannels.CreateCountEdgesChannel(ciChannel, "", CICountEdgesActiveEdge.Rising, 0, CICountEdgesCountDirection.Up);
                ciTask.Control(TaskAction.Verify);

                ciTask.Timing.SampleClockRate = mConfig.ScanProperty.InputSampleRate;
                ciTask.Timing.ConfigureSampleClock(ciClock,
                    ciTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    Z1Sequence.InputSampleCountPerFrame);

                ciTask.Stream.ConfigureInputBuffer(Z1Sequence.InputSampleCountPerFrame);

                // CIDataTransferMechanism x = ciTask.CIChannels.All.DataTransferMechanism;

                // 指定CI Channel使用的物理输入终端[APD的脉冲接收端]
                ciTask.CIChannels[0].CountEdgesTerminal = ciSrc;

                // 设置Ci Pause Trigger源为PFIx，PFIx与Acq Trigger[一般是Do]物理直连，接收Do的输出信号，作为触发
                // 低电平使能Pause Trigger,高电平禁能
                // ciTask.Triggers.PauseTrigger.ConfigureDigitalLevelTrigger(ciGate, DigitalLevelPauseTriggerCondition.Low);

                // 设置Arm Start Trigger，使CI与AO、DO同时启动工作
                string source = mSettings.GetStartSyncSignal();
                ciTask.Triggers.ArmStartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeArmStartTriggerEdge.Rising);

                ciTask.EveryNSamplesReadEventInterval = Z1Sequence.InputSampleCountPerAcquisition;
                ciTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(CiEveryNSamplesRead);

                ciMultiChannelReader = new CounterSingleChannelReader(ciTask.Stream);
                ciMultiChannelReader.SynchronizeCallbacks = false;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ci task[{0}] exception: [{1}].", ciChannel, e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_CI_TASK_EXCEPTION;
            }

            return code;
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
                string source = mSettings.GetPmtTriggerInPfi();
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
                mAiTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(AiEveryNSamplesRead);

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
        /// 使用PMT时，生成激活的通道对应的模拟通道序列号
        /// </summary>
        public void GenerateAiChannelIndex()
        {
            int index = -1;
            int channelNum = mConfig.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                mAiChannelIndex[i] = mConfig.ScanProperty.ScanChannels[i].ChannelSwitch == CHAN_SWITCH.ON ? ++index : -1;
            }
        }

        public int GetLaserAiChannelIndex(int channelId)
        {
            return mAiChannelIndex[channelId];
        }

        /// <summary>
        /// 获取AO物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetAoPhysicalChannelName()
        {
            string physicalChannelName = string.Empty;
            if (mConfig.ScanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
            {
                physicalChannelName = string.Concat(mSettings.GetXGalvoAoChannel(), ",", mSettings.GetYGalvoAoChannel(), ",", mSettings.GetY2GalvoAoChannel());
            }
            else
            {
                physicalChannelName = string.Concat(mSettings.GetXGalvoAoChannel(), ",", mSettings.GetYGalvoAoChannel());
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
            return mSettings.GetAcqTriggerDoLine();
        }

        /// <summary>
        /// 获取CI物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetCiSampleClockSourceName()
        {
            string aoDeviceName = mSettings.GetXGalvoAoChannel().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
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
                if (mConfig.ScanProperty.ScanChannels[i].ChannelSwitch == CHAN_SWITCH.ON)
                {
                    activatedChannelNames.Add(mSettings.GetPmtAiChannel((CHAN_ID)i));
                }
            }
            string physicalChannelName = string.Join(",", activatedChannelNames.ToArray());
            Logger.Info(string.Format("ai physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
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

        private void CiEveryNSamplesRead(object sender, EveryNSamplesReadEventArgs e)
        {
            try
            {
                int index = FindCiMultiChannelReaderIndex(sender);
                if (index < 0)
                {
                    Logger.Warn(string.Format("no matching ci task found: [{0}].", sender));
                    return;
                }

                int[] originSamples = mCiChannelReaders[index].ReadMultiSampleInt32(Z1Sequence.InputSampleCountPerAcquisition);

                if (CiSamplesReceived != null)
                {
                    CiSamplesReceived.Invoke(this, index, originSamples);
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
        }

        private void AiEveryNSamplesRead(object sender, EveryNSamplesReadEventArgs e)
        {
            try
            {
                // 读取16位原始数据，每次读取单次采集的像素数
                int channelNum = mConfig.GetChannelNum();
                short[,] originSamples = mAiUnscaledReader.ReadInt16(Z1Sequence.InputSampleCountPerAcquisition);
                AnalogWaveform<short>[] waves = AnalogWaveform<short>.FromArray2D(originSamples);

                short[][] samples = new short[channelNum][];
                for (int i = 0; i < channelNum; i++)
                {
                    samples[i] = GetLaserAiChannelIndex(i) >= 0 ? waves[GetLaserAiChannelIndex(i)].GetRawData() : null;
                }

                if (AiSamplesReceived != null)
                {
                    AiSamplesReceived.Invoke(this, samples);
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
        }

    }
}