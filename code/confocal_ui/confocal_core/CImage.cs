using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class CImage
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static void Gray16ToGray8(ushort[] source, out byte[] destnation)
        {
            destnation = new byte[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                destnation[i] = (byte)(source[i] >> 8);
            }
        }

        public static void Gray16ToBGR24(ushort[] source, out byte[] destnation)
        {
            int i, j;
            byte value;

            destnation = new byte[source.Length * 3];
            
            for (i = 0; i < source.Length; i++)
            {
                value = (byte)(source[i] >> 8);
                j = i * 3;

                if (value < 64)
                {
                    destnation[j + 2] = 0;                      // R
                    destnation[j + 1] = (byte)(value << 2);     // G
                    destnation[j] = 255;              // B
                }
                else if (value < 128)
                {
                    destnation[j + 2] = 0;
                    destnation[j + 1] = 255;
                    destnation[j] = (byte)(255 - ((value - 64) << 2));
                }
                else if (value < 192)
                {
                    destnation[j + 2] = (byte)((value - 128) << 2);
                    destnation[j + 1] = 255;
                    destnation[j] = 0;
                }
                else
                {
                    destnation[j + 2] = 255;
                    destnation[j + 1] = (byte)(255 - ((source[i] - 192) << 2));
                    destnation[j] = 0;
                }
            }
        }

    }
}
