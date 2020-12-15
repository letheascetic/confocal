using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class Z1Sequence
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private double[] mTimeSequence;
        private double[] mXSequence;
        private double[] mYSequence;
        private double[] mTSequence;
        private int mXCycles;

        public Z1Sequence(Z1ScanProperty scanProperty)
        {
            // Z1ScanField extendScanField = scanProperty.GetExtendScanField();
            RectangleF extendScanRange = scanProperty.GetExtendScanField().ScanRange;

            if (scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION)
            {
                int lineStartSampleCount = (int)(scanProperty.ScanLineStartTime / (int)scanProperty.ScanPixelDwell);
                double a = extendScanRange.Width / Z1ScanField.ExtendLineMarginDiv;
                double w0 = Math.PI / scanProperty.ScanLineStartTime * (int)scanProperty.ScanPixelDwell;
                double[] lineSatrtSamples = new double[lineStartSampleCount];
                for (int n = 0; n < lineStartSampleCount; n++)
                {
                    lineSatrtSamples[n] = extendScanRange.X - a * Math.Sin(w0 * n);
                }

                int lineHoldSampleCount = (int)(scanProperty.ScanLineHoldTime / (int)scanProperty.ScanPixelDwell);
                double w1 = Math.PI / scanProperty.ScanLineHoldTime * (int)scanProperty.ScanPixelDwell;
                double[] lineHoldSamples = new double[lineHoldSampleCount];
                for (int n = 0; n < lineHoldSampleCount; n++)
                {
                    lineHoldSamples[n] = extendScanRange.Right + a * Math.Sin(w1 * n);
                }

                int lineEndSampleCount = (int)(scanProperty.ScanLineEndTime / (int)scanProperty.ScanPixelDwell);
                double[] lineEndSamples = CreateLinearArray(extendScanRange.Right, extendScanRange.X, lineEndSampleCount);

                int lineScanSampleCount = (int)scanProperty.ScanPixels;
                double[] lineScanSamples = CreateLinearArray(extendScanRange.X, extendScanRange.Right, lineScanSampleCount);
                
                int lineSamples = lineStartSampleCount + lineHoldSampleCount + lineEndSampleCount + lineScanSampleCount;
                mXSequence = new double[lineSamples];
                Array.Copy(lineSatrtSamples, 0, mXSequence, 0, lineStartSampleCount);
                Array.Copy(lineScanSamples, 0, mXSequence, lineStartSampleCount, lineScanSampleCount);
                Array.Copy(lineHoldSamples, 0, mXSequence, lineStartSampleCount + lineScanSampleCount, lineHoldSampleCount);
                Array.Copy(lineEndSamples, 0, mXSequence, lineScanSampleCount - lineEndSampleCount, lineEndSampleCount);

            }
        }

        public static double[] CreateLinearArray(double start, double end, int count)
        {
            double[] array = new double[count];
            double step = (end - start) / count;
            for (int i = 0; i < count; i++)
            {
                array[i] = start + step * i;
            }
            return array;
        }



    }
}
