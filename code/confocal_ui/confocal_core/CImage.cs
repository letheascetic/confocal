using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace confocal_core
{
    public class CImage
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        public static void CreateColorMapping(Color color, ref byte[,] colorMapping)
        {
            float rCoff = color.R / 256.0f;
            float gCoff = color.G / 256.0f;
            float bCoff = color.B / 256.0f;

            byte value;
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                value = (byte)i;
                colorMapping[i, 2] = (byte)(rCoff * value);
                colorMapping[i, 1] = (byte)(gCoff * value);
                colorMapping[i, 0] = (byte)(bCoff * value);
            }
        }

        public static void Gray16ToGray8(ushort[] source, out byte[] destnation)
        {
            destnation = new byte[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                destnation[i] = (byte)(source[i] >> 8);
            }
        }

        public static void Gray16ToGray24(ushort[] source, out byte[] destnation)
        {
            destnation = new byte[source.Length * 3];
            for (int i = 0, j = 0; i < source.Length; i++)
            {
                j = i * 3;
                destnation[j] = (byte)(source[i] >> 8);
                destnation[j + 1] = destnation[j];
                destnation[j + 2] = destnation[j];
            }
        }

        public static void Gray16ToBGR24(short[] source, out byte[] destnation)
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

        public static void Gray16ToBGR24(Color color, short[] source, out byte[] destnation)
        {
            float rCoff = color.R / 256.0f;
            float gCoff = color.G / 256.0f;
            float bCoff = color.B / 256.0f;

            destnation = new byte[source.Length * 3];

            int i, j;
            byte value;

            for (i = 0; i < source.Length; i++)
            {
                j = i * 3;

                value = (byte)(source[i] >> 8);

                destnation[j + 2] = (byte)(rCoff * value);
                destnation[j + 1] = (byte)(gCoff * value);
                destnation[j] = (byte)(bCoff * value);
            }
        }

        public static void Gray16ToBGR24(short[] source, ref byte[] destnation, int destIndex, byte[,] mapping)
        {
            byte value;
            int i, j;
            for (i = 0; i < source.Length; i++)
            {
                value = (byte)(source[i] >> 8);
                j = i * 3 + destIndex;
                destnation[j] = mapping[value, 0];
                destnation[j + 1] = mapping[value, 1];
                destnation[j + 2] = mapping[value, 2];
            }
        }

        public static void Gray16ToBGR24(int[] source, ref byte[] destnation, int destIndex, byte[,] mapping)
        {
            byte value;
            int i, j;
            for (i = 0; i < source.Length; i++)
            {
                value = (byte)(source[i] >> 8);
                j = i * 3 + destIndex;
                destnation[j] = mapping[value, 0];
                destnation[j + 1] = mapping[value, 1];
                destnation[j + 2] = mapping[value, 2];
            }
        }

        public static Bitmap CreateBitmap(byte[] data, int width, int height)
        {
            Bitmap Canvas = new Bitmap(width, height);
            BitmapData CanvasData = Canvas.LockBits(new System.Drawing.Rectangle(0, 0, Canvas.Width, Canvas.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            IntPtr ptr = CanvasData.Scan0;
            Marshal.Copy(data, 0, ptr, data.Length);
            Canvas.UnlockBits(CanvasData);
            return Canvas;
        }
    }
}
