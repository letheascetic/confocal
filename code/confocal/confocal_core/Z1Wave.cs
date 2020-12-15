using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class Z1Wave
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static byte[] TriggerWave { get; set; }
        public static double[] XWave { get; set; }
        public static double[] Y1Wave { get; set; }
        public static double[] Y2Wave { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static void GenerateWave()
        {
            
        }

    }
}
