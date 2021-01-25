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

        private ConfigViewModel mConfig;

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

        /// <summary>
        /// 初始化增益控制板卡
        /// </summary>
        public void ConfigUsbDac()
        {
            UsbDac.Connect();
        }

        /// <summary>
        /// 初始化激光器
        /// </summary>
        public void ConfigLaser()
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

        public void ReleaseLaser()
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

        public void ReleaseUsbDac()
        {
            if (UsbDac.IsConnected())
            {
                UsbDac.Release();
            }
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

    }
}
