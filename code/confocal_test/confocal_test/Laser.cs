using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Confocal
{
    struct LaserChannel
    {
        public int id;
        public int status;
        public float power;
    };

    public static class Laser
    {
        /************************************************************************************/
        public static readonly int LASER_CHAN_ID_405_NM = 0x0000;
        public static readonly int LASER_CHAN_ID_488_NM = 0x0001;
        public static readonly int LASER_CHAN_ID_561_NM = 0x0002;
        public static readonly int LASER_CHAN_ID_640_NM = 0x0003;
        public static readonly int LASER_CHAN_SWITCH_OFF = 0x0000;
        public static readonly int LASER_CHAN_SWITCH_ON = 0x0001;
        /************************************************************************************/
        private static readonly int LASER_CHAN_NUM = 4;
        private static readonly float LASER_POWER_DEFAULT = 10.0f;
        /************************************************************************************/
        [DllImport("laserlib2.dll")]
        private static extern bool LaserLib2_Open(string portName);
        [DllImport("laserlib2.dll")]
        private static extern bool LaserLib2_Close();
        [DllImport("laserlib2.dll")]
        private static extern bool LaserLib2_Active(int channel, bool fOn);
        [DllImport("laserlib2.dll")]
        private static extern bool LaserLib2_SetPower(int channel, float power);
        [DllImport("laserlib2.dll")]
        private static extern bool LaserLib2_SetParam(float p1, float p2, float p3, float p4, float c1, float c2, float c3, float c4);
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/
        private static string m_portName;
        private static bool m_connected;
        private static LaserChannel[] m_channels;
        /************************************************************************************/

        static Laser()
        {
            m_portName = null;
            m_connected = false;
            m_channels = new LaserChannel[LASER_CHAN_NUM];
            for (int i = 0; i < LASER_CHAN_NUM; i++)
            {
                m_channels[i].id = i;
                m_channels[i].status = LASER_CHAN_SWITCH_OFF;
                m_channels[i].power = LASER_POWER_DEFAULT;
            }
        }

        public static string PortName()
        {
            return m_portName;
        }

        public static bool IsConnected()
        {
            return m_connected;
        }

        public static int Connect(string portName)
        {
            if (m_connected == true)
            {
                Logger.Info(string.Format("Laser already connected."));
                return (int)RETURN_CODE.API_SUCCESS;
            }

            if (!LaserLib2_Open(portName))
            {
                Logger.Info(string.Format("Laser connect failed:[LaserLib2_Open][{0}].", RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED));
                return (int)RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
            }
            if (!LaserLib2_SetParam(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f))
            {
                Logger.Info(string.Format("Laser connect failed:[LaserLib2_SetParam][{0}].", RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED));
                return (int)RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
            }
            m_connected = true;
            Logger.Info(string.Format("Laser connect success:[{0}].", RETURN_CODE.API_SUCCESS));
            return (int)RETURN_CODE.API_SUCCESS;
        }

        public static int Release()
        {
            if (m_connected == false)
            {
                Logger.Info(string.Format("Laser already released."));
                return (int)RETURN_CODE.API_SUCCESS;
            }

            if (!LaserLib2_Close())
            {
                Logger.Info(string.Format("Laser release failed:[LaserLib2_Close][{0}].", RETURN_CODE.API_FAILED_LASER_RELEASE_FAILED));
                return (int)RETURN_CODE.API_FAILED_LASER_RELEASE_FAILED;
            }
            m_connected = false;
            Logger.Info(string.Format("Laser release success:[{0}].", RETURN_CODE.API_SUCCESS));
            return (int)RETURN_CODE.API_SUCCESS;
        }

        public static int OpenChannel(int id)
        {
            if (m_channels[id].status == LASER_CHAN_SWITCH_ON)
            {
                Logger.Info(string.Format("Laser channel [{0}] already open.", id));
                return (int)RETURN_CODE.API_SUCCESS;
            }

            if(!LaserLib2_Active(id + 1, true))
            {
                // m_channels[id].status = LASER_CHAN_SWITCH_OFF;
                Logger.Info(string.Format("Laser open channel [{0}] failed:[LaserLib2_Active][{1}].", id, RETURN_CODE.API_FAILED_LASER_OPEN_CHANNEL_FAILED));
                return (int)RETURN_CODE.API_FAILED_LASER_OPEN_CHANNEL_FAILED;
            }

            m_channels[id].status = LASER_CHAN_SWITCH_ON;
            Logger.Info(string.Format("Laser open channel [{0}] success:[LaserLib2_Active][{1}].", id, RETURN_CODE.API_SUCCESS));
            return (int)RETURN_CODE.API_SUCCESS;
        }

        public static int CloseChannel(int id)
        {
            if (m_channels[id].status == LASER_CHAN_SWITCH_OFF)
            {
                Logger.Info(string.Format("Laser channel [{0}] already closed.", id));
                return (int)RETURN_CODE.API_SUCCESS;
            }

            if (!LaserLib2_Active(id + 1, false))
            {
                Logger.Info(string.Format("Laser close channel [{0}] failed:[LaserLib2_Active][{1}].", id, RETURN_CODE.API_FAILED_LASER_CLOSE_CHANNEL_FAILED));
                return (int)RETURN_CODE.API_FAILED_LASER_CLOSE_CHANNEL_FAILED;
            }

            m_channels[id].status = LASER_CHAN_SWITCH_OFF;
            Logger.Info(string.Format("Laser close channel [{0}] success:[LaserLib2_Active][{1}].", id, RETURN_CODE.API_SUCCESS));
            return (int)RETURN_CODE.API_SUCCESS;
        }



    }
}
