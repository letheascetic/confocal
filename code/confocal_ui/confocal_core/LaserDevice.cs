using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace confocal_core
{
    public static class LaserDevice
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("laserlib2.dll")]
        static extern bool LaserLib2_Open(string portName);
        [DllImport("laserlib2.dll")]
        static extern bool LaserLib2_Close();
        [DllImport("laserlib2.dll")]
        static extern bool LaserLib2_Active(int channel, bool fOn);
        [DllImport("laserlib2.dll")]
        static extern bool LaserLib2_SetPower(int channel, float power);
        [DllImport("laserlib2.dll")]
        static extern bool LaserLib2_SetParam(float p1, float p2, float p3, float p4, float c1, float c2, float c3, float c4);
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static Config m_config;
        private static bool m_connected;
        ///////////////////////////////////////////////////////////////////////////////////////////
        static LaserDevice()
        {
            m_connected = false;
            m_config = Config.GetConfig();
        }
        
        public static bool IsConnected()
        {
            return m_connected;
        }
        
        /// <summary>
        /// 连接激光端口
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static API_RETURN_CODE Connect(string portName)
        {
            try
            {
                if (!LaserLib2_Open(portName))
                {
                    Logger.Info(string.Format("Laser connect failed:[LaserLib2_Open][{0}].", API_RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED));
                    return API_RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
                }
                if (!LaserLib2_SetParam(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f))
                {
                    Logger.Error(string.Format("Laser connect failed:[LaserLib2_SetParam][{0}].", API_RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED));
                    return API_RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
                }
                m_connected = true;
                Logger.Info(string.Format("Laser connect success:[{0}].", API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Laser connect exception: [{0}].", e));
                return API_RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
            }
        }

        /// <summary>
        /// 关闭激光端口
        /// </summary>
        /// <returns></returns>
        public static API_RETURN_CODE Release()
        {
            if (m_connected == false)
            {
                Logger.Info(string.Format("Laser already released."));
                return API_RETURN_CODE.API_SUCCESS;
            }

            try
            {
                if (!LaserLib2_Close())
                {
                    Logger.Error(string.Format("Laser release failed:[LaserLib2_Close][{0}].", API_RETURN_CODE.API_FAILED_LASER_RELEASE_FAILED));
                    return API_RETURN_CODE.API_FAILED_LASER_RELEASE_FAILED;
                }
                m_connected = false;
                Logger.Info(string.Format("Laser release success:[{0}].", API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Laser release exception: [{0}].", e));
                return API_RETURN_CODE.API_FAILED_LASER_RELEASE_FAILED;
            }
        }
        
        /// <summary>
        /// 打开激光通道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static API_RETURN_CODE OpenChannel(CHAN_ID id)
        {
            int channel = GetChannelIndex(id);
            try
            {
                if (!LaserLib2_Active(channel, true))
                {
                    Logger.Error(string.Format("Laser open channel[{0}] failed:[LaserLib2_Active][{1}].", id, API_RETURN_CODE.API_FAILED_LASER_OPEN_CHANNEL_FAILED));
                    return API_RETURN_CODE.API_FAILED_LASER_OPEN_CHANNEL_FAILED;
                }
                Logger.Info(string.Format("Laser open channel[{0}] success:[LaserLib2_Active][{1}].", id, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Laser open channel[{0}] exception: [{1}].", id, e));
                return API_RETURN_CODE.API_FAILED_LASER_OPEN_CHANNEL_FAILED;
            }
        }

        /// <summary>
        /// 关闭激光通道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static API_RETURN_CODE CloseChannel(CHAN_ID id)
        {
            int channel = GetChannelIndex(id);
            try
            {
                if (!LaserLib2_Active(channel, false))
                {
                    Logger.Error(string.Format("Laser close channel[{0}] failed:[LaserLib2_Active][{1}].", id, API_RETURN_CODE.API_FAILED_LASER_CLOSE_CHANNEL_FAILED));
                    return API_RETURN_CODE.API_FAILED_LASER_CLOSE_CHANNEL_FAILED;
                }
                Logger.Info(string.Format("Laser close channel[{0}] success:[LaserLib2_Active][{1}].", id, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Laser close channel[{0}] exception: [{1}].", id, e));
                return API_RETURN_CODE.API_FAILED_LASER_CLOSE_CHANNEL_FAILED;
            }
        }

        /// <summary>
        /// 设置激光功率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static API_RETURN_CODE SetChannelPower(CHAN_ID id, double power)
        {
            int channel = GetChannelIndex(id);
            int configValue = PowerToConfigValue(power);
            try
            {
                if (!LaserLib2_SetPower(channel, configValue))
                {
                    Logger.Error(string.Format("Laser set channel[{0}] power[{1}] failed:[LaserLib2_SetPower][{2}].", id, power, API_RETURN_CODE.API_FAILED_LASER_SET_POWER_FAILED));
                    return API_RETURN_CODE.API_FAILED_LASER_SET_POWER_FAILED;
                }
                Logger.Info(string.Format("Laser set channel[{0}] power[{1}] success:[LaserLib2_SetPower][{2}].", id, power, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Laser set channel[{0}] power[{1}] exception: [{2}].", id, power, e));
                return API_RETURN_CODE.API_FAILED_LASER_SET_POWER_FAILED;
            }
        }

        public static int PowerToConfigValue(double power)
        {
            return (int)(power * 100);
        }

        public static double ConfigValueToPower(int configValue)
        {
            return (double)configValue / 100.0;
        }

        private static int GetChannelIndex(CHAN_ID id)
        {
            switch (id)
            {
                case CHAN_ID.WAVELENGTH_405_NM:
                    return 2;
                case CHAN_ID.WAVELENGTH_488_NM:
                    return 1;
                case CHAN_ID.WAVELENGTH_561_NM:
                    return 4;
                case CHAN_ID.WAVELENGTH_640_NM:
                    return 3;
                default:
                    return 0;
            }
        }

    }
}
