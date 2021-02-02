using confocal_core.Model;
using Emgu.CV;
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
        /// <summary>
        /// 负电压转换成正电压
        /// </summary>
        /// <param name="samples"></param>
        public static void ToPositive(ref ushort[] samples)
        {
            for (int i = 0; i < samples.Length; i++)
            {
                if (samples[i] >= short.MaxValue)
                {
                    samples[i] = (ushort)(ushort.MaxValue - samples[i]);
                }
            }
        }

        /// <summary>
        /// 将单次采集的样本转换成矩阵数据
        /// </summary>
        /// <param name="samples">单次采集的样本</param>
        /// <param name="samplesPerPixel">单像素包含的样本数，每个像素等于多个样本的和</param>
        /// <param name="pixelsPerRow">矩阵单行包含的像素数</param>
        /// <param name="pixelsPerCol">矩阵包含的行数</param>
        /// <returns></returns>
        public static NDArray ToMatrix(ushort[] samples, int samplesPerPixel, int pixelsPerRow, int pixelsPerCol, int scanDirection)
        {
            // create NDArray，no copy
            var origin = np.array(samples, false).reshape(samplesPerPixel, pixelsPerRow, pixelsPerCol);
            // integrate pixels 样本累加，计算像素值，并转置
            var matrix = origin.sum(0).T;
            // 如果是双向扫描，则翻转偶数行
            if (scanDirection == ScanDirectionModel.BIDIRECTION)
            {
                var cy = matrix.copy();
                matrix["1::2"] = cy["1::2", "::-1"];
            }
            return matrix;
        }

        /// <summary>
        /// 将矩阵数据转换成Bank图像数据
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="image"></param>
        public static void ToBankImage(NDArray matrix, ref Mat image, int pixelOffset, int pixelCali)
        {
            
        }

    }
}
