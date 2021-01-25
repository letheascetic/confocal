using confocal_core.Model;
using confocal_core.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    public class Scheduler
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Scheduler pScheduler = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private NiDaq mNiDaq;
        private ConfigViewModel mConfig;

        private List<ScanTask> mScanTasks;
        private ScanTask mScanningTask;

        public List<ScanTask> ScanTasks
        {
            get { return mScanTasks; }
            set { mScanTasks = value; }
        }

        public ScanTask ScanningTask
        {
            get { return mScanningTask; }
            set { mScanningTask = value; }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        public static Scheduler CreateInstance()
        {
            if (pScheduler == null)
            {
                lock (locker)
                {
                    if (pScheduler == null)
                    {
                        pScheduler = new Scheduler();
                    }
                }
            }
            return pScheduler;
        }

        public void Initialize()
        {
            mConfig = ConfigViewModel.GetConfig();

            ConfigUsbDac();
            ConfigLaser();
        }

        public void Release()
        {
            ReleaseLaser();
            ReleaseUsbDac();
        }

        public API_RETURN_CODE CreateScanTask(int taskId, string taskName, out ScanTask scanTask)
        {
            scanTask = FindScanTask(taskId);
            if (scanTask == null)
            {
                scanTask = new ScanTask(taskId, taskName);
                ScanTasks.Add(scanTask);
            }

            Logger.Info(string.Format("create scan task[{0}|{1}].", taskId, taskName));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE StartScanTask(ScanTask scanTask)
        {
            if (scanTask == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_INVALID;
            }

            if (FindScanTask(scanTask.TaskId) == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_NOT_FOUND;
            }

            ScanningTask = scanTask;

            // TO DO: 

            API_RETURN_CODE code = mNiDaq.Start();      // 启动板卡
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Logger.Info(string.Format("start scan task[{0}|{1}] failed: [{2}].", scanTask.TaskId, scanTask.TaskName, code));
                StopScanTask();
                return code;
            }

            Logger.Info(string.Format("start scan task[{0}|{1}] success.", scanTask.TaskId, scanTask.TaskName));
            return code;
        }

        public API_RETURN_CODE StopScanTask()
        {
            if (ScanningTask == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_INVALID;
            }

            if (FindScanTask(ScanningTask.TaskId) == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_NOT_FOUND;
            }

            mNiDaq.Stop();
            ScanningTask.Stop();
            ScanningTask = null;

            Logger.Info(string.Format("stop scan task[{0}|{1}].", ScanningTask.TaskId, ScanningTask.TaskName));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public ScanTask FindScanTask(int taskId)
        {
            return ScanTasks.Where(p => p.TaskId == taskId).FirstOrDefault();
        }

        /// <summary>
        /// 在属性变化前执行
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE BeforePropertyChanged()
        {
            // 如果当前正在采集(有任一采集模式使能)，则先停止采集
            if (mConfig.IsScanning)
            {
                // TO DO：停止采集
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 改变通道激光功率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelPowerChangeCommand(ScanChannelModel channel)
        {
            if (Laser.IsConnected())
            {
                return Laser.SetChannelPower(channel.ID, Laser.ConfigValueToPower(channel.LaserPower));
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 改变通道增益
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelGainChangeCommand(ScanChannelModel channel)
        {
            return UsbDac.SetDacOut((uint)channel.ID, UsbDac.ConfigValueToVout(channel.Gain));
        }

        /// <summary>
        /// 打开或关闭通道激光
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activated"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelActivateChangeCommand(ScanChannelModel channel)
        {
            if (Laser.IsConnected())
            {
                if (channel.Activated)
                {
                    Laser.OpenChannel(channel.ID);
                    return Laser.SetChannelPower(channel.ID, Laser.ConfigValueToPower(channel.LaserPower));
                }
                else
                {
                    Laser.CloseChannel(channel.ID);
                }
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        private Scheduler()
        {
            mConfig = ConfigViewModel.GetConfig();
            Initialize();
        }

        /// <summary>
        /// 初始化增益控制板卡
        /// </summary>
        private void ConfigUsbDac()
        {
            UsbDac.Connect();
        }

        /// <summary>
        /// 初始化激光器
        /// </summary>
        private void ConfigLaser()
        {
            string portName = mConfig.LaserPort;
            API_RETURN_CODE code = Laser.Connect(portName);

            for (int i = 0; i < mConfig.GetChannelNum(); i++)
            {
                if (!mConfig.ScanChannels[i].Activated)
                {
                    continue;
                }
                if (Laser.OpenChannel(i) != API_RETURN_CODE.API_SUCCESS)
                {
                    mConfig.ScanChannels[i].Activated = false;
                }
                Laser.SetChannelPower(i, mConfig.ScanChannels[i].LaserPower);
            }
        }

        private void ReleaseLaser()
        {
            if (Laser.IsConnected())
            {
                for (int i = 0; i < mConfig.GetChannelNum(); i++)
                {
                    if (mConfig.ScanChannels[i].Activated)
                    {
                        Laser.CloseChannel(i);
                        mConfig.ScanChannels[i].Activated = false;
                    }
                }
            }
            Laser.Release();
        }

        private void ReleaseUsbDac()
        {
            if (UsbDac.IsConnected())
            {
                UsbDac.Release();
            }
        }

    }
}
