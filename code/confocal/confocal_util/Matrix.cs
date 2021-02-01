using Emgu.CV;
using NumSharp;
using System;

namespace confocal_util
{
    public class Matrix
    {

        public static Mat ConvertToMatrix(ushort[] samples, int samplesPerPixel, int pixelsPerRow, int pixelsPerCol)
        {
            var origin = np.array(samples).reshape(samplesPerPixel, pixelsPerRow, pixelsPerCol);
            var x = origin.sum(0);
            return new Mat();
        }
    }
}
