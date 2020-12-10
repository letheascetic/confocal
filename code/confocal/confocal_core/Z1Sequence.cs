using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class Z1Sequence<T>
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private T[] mXSequence;
        private T[] mYSequence;
        private T[] mTSequence;
        private int mXCycles;

        public Z1Sequence(Z1ScanProperty scanProperty)
        {
            RectangleF scanField = scanProperty.ScanFields.GetScanField(scanProperty.ScanArea);
            

        }



    }
}
