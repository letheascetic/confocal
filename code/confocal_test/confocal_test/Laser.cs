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
        UInt16 id;
        UInt16 status;
        float power;
    };

    public static class Laser
    {
        /************************************************************************************/
        public static readonly UInt16 LASER_CHAN_ID_405_NM = 0x0000;
        public static readonly UInt16 LASER_CHAN_ID_488_NM = 0x0001;
        public static readonly UInt16 LASER_CHAN_ID_561_NM = 0x0002;
        public static readonly UInt16 LASER_CHAN_ID_640_NM = 0x0003;
        public static readonly UInt16 LASER_CHAN_SWITCH_OFF = 0x0000;
        public static readonly UInt16 LASER_CHAN_SWITCH_ON = 0x0001;
        /************************************************************************************/
        private static readonly UInt16 LASER_CHAN_NUM = 4;
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

        /************************************************************************************/
        private static string portName = null;
        private static bool m_connected = false;
        private static LaserChannel[] channels = new LaserChannel[4];
        /************************************************************************************/

        static Laser()
        {

        }

        public static bool IsConnected()
        {
            return m_connected;
        }

        public static int Connect(string portName)
        {
            if (!LaserLib2_Open(portName))
            {
                return (int)RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
            }
            if (!LaserLib2_SetParam(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f))
            {
                return (int)RETURN_CODE.API_FAILED_LASER_CONNECT_FAILED;
            }
            m_connected = true;
            return (int)RETURN_CODE.API_SUCCESS;
        }

        public static int Release()
        {
            if (!LaserLib2_Close())
            {
                return (int)RETURN_CODE.API_FAILED_LASER_RELEASE_FAILED;
            }
            m_connected = false;
            return (int)RETURN_CODE.API_SUCCESS;
        }

    }
}
