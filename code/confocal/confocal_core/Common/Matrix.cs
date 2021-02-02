using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace confocal_core.Common
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
    }
}
