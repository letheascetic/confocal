using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace confocal_test
{
    public static class Laser
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
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


        


    }
}
