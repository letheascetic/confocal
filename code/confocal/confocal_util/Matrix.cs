using Emgu.CV;
using NumSharp;
using System;

namespace confocal_util
{
    public class Matrix
    {
        public static NDArray ConvertToMatrix(ushort[] samples, int samplesPerPixel, int pixelsPerRow, int pixelsPerCol)
        {
            var origin = np.array(samples).reshape(samplesPerPixel, pixelsPerRow, pixelsPerCol);
            var bankMatrix = origin.sum(0);
            bankMatrix["1::2"] = bankMatrix["1::2, ::-1"];
            return bankMatrix;
        }

        public static void Print2D(NDArray array)
        {
            for (int i = 0; i < array.Shape[0]; i++)
            {
                
            }
        }

    }
}
