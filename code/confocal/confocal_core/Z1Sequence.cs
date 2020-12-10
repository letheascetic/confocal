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

        private T[] mTimeSequence;
        private T[] mXSequence;
        private T[] mYSequence;
        private T[] mTSequence;
        private int mXCycles;

        public Z1Sequence(Z1ScanProperty scanProperty)
        {
            Z1ScanField extendScanField = scanProperty.GetExtendScanField();

            if (scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION)
            {
                int lineStartSamples = (int)(scanProperty.ScanLineStartTime / (int)scanProperty.ScanPixelDwell);
                int lineHoldSamples = (int)(scanProperty.ScanLineHoldTime / (int)scanProperty.ScanPixelDwell);
                int lineEndSamples = (int)(scanProperty.ScanLineEndTime / (int)scanProperty.ScanPixelDwell);

            }



        }



    }
}
