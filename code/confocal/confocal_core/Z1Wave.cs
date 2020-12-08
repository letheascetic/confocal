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
        private volatile static Z1Wave pMave = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public byte[] TriggerWave { get; set; }
        public double[] XWave { get; set; }
        public double[] Y1Wave { get; set; }
        public double[] Y2Wave { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static Z1Wave GetScanWave()
        {
            if (pMave == null)
            {
                lock (locker)
                {
                    if (pMave == null)
                    {
                        pMave = new Z1Wave();
                    }
                }
            }
            return pMave;
        }



    }
}
