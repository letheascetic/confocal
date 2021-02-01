using Emgu.CV;
using NumSharp;
using System;

namespace confocal_util
{
    public class Matrix
    {

        public static Mat ConvertToMatrix(ushort[] samples, int samplesPerPixel, int samplesPerRow, int sampleCount)
        {
            var nd = np.array(samples);
            return new Mat();
        }


    }
}
