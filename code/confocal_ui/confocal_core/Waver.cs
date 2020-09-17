using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace confocal_core
{
    /// <summary>
    /// 扫描波形生成器
    /// </summary>
    public class Waver
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Waver m_waver = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public byte[] TriggerWave { get; set; }
        public double[] XWave { get; set; }
        public double[] Y1Wave { get; set; }
        public double[] Y2Wave { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region apis

        public static Waver GetWaver()
        {
            if (m_waver == null)
            {
                lock (locker)
                {
                    if (m_waver == null)
                    {
                        m_waver = new Waver();
                    }
                }
            }
            return m_waver;
        }

        /// <summary>
        /// 生成扫描控制波形
        /// </summary>
        /// <param name="m_params"></param>
        public void Generate()
        {
            if (Config.GetConfig().GetScanStrategy() == SCAN_STRATEGY.Z_UNIDIRECTION)
            {
                GenerateSScan();
            }
            else
            {
                GenerateBScan();
            }
        }

        private Waver()
        {

        }

        /// <summary>
        /// 生成双向扫描控制波形数据
        /// </summary>
        private void GenerateBScan()
        {
            Initialize();
            Params m_params = Params.GetParams();

            Array.Copy(m_params.DigitalTriggerSamplesPerLine, TriggerWave, m_params.DoSampleCountPerLine);
            int offset = -m_params.AoSampleCountPerLine;
            int firstYSampleCount = m_params.AoPreviousSampleCountPerLine + m_params.AoValidSampleCountPerLine;
            int secondYSampleCount = m_params.AoSampleCountPerLine - firstYSampleCount;

            for (int i = 0; i < m_params.ScanRows; i++)
            {
                offset += m_params.AoSampleCountPerLine;
                Array.Copy(m_params.AoXSamplesPerLine, 0, XWave, offset, m_params.AoSampleCountPerLine);

                Array.Copy(Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[2 * i], firstYSampleCount).ToArray(), 0, Y1Wave, offset, firstYSampleCount);
                Array.Copy(Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[2 * i + 1], secondYSampleCount).ToArray(), 0, Y1Wave, offset + firstYSampleCount, secondYSampleCount);

                Array.Copy(Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[2 * i], firstYSampleCount).ToArray(), 0, Y2Wave, offset, firstYSampleCount);
                Array.Copy(Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[2 * i + 1], secondYSampleCount).ToArray(), 0, Y2Wave, offset + firstYSampleCount, secondYSampleCount);

                //for (int j = 0; j < m_params.AoSampleCountPerLine; j++)
                //{
                //    index = offset + j;
                //    halfIndex = m_params.AoPreviousSampleCountPerLine + m_params.AoValidSampleCountPerLine;

                //    if (j < halfIndex)
                //    {
                //        Y1Wave[index] = m_params.AoY1SamplesPerRow[i];
                //        Y2Wave[index] = m_params.AoY2SamplesPerRow[i];
                //    }
                //    else
                //    {
                //        Y1Wave[index] = m_params.AoY1SamplesPerRow[i] + m_params.AoVoltagePerPixel;
                //        Y2Wave[index] = Y1Wave[index] * 2;
                //    }
                //}
            }

        }

        /// <summary>
        /// 生成单向扫描控制波形数据
        /// </summary>
        private void GenerateSScan()
        {
            Initialize();
            Params m_params = Params.GetParams();

            Array.Copy(m_params.DigitalTriggerSamplesPerLine, TriggerWave, m_params.DoSampleCountPerLine);

            int offset = -m_params.AoSampleCountPerLine;
            for (int i = 0; i < m_params.ScanRows; i++)
            {
                offset += m_params.AoSampleCountPerLine;
                Array.Copy(m_params.AoXSamplesPerLine, 0, XWave, offset, m_params.AoSampleCountPerLine);
                Array.Copy(Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[i], m_params.AoSampleCountPerLine).ToArray(), 0, Y1Wave, offset, m_params.AoSampleCountPerLine);
                Array.Copy(Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[i], m_params.AoSampleCountPerLine).ToArray(), 0, Y2Wave, offset, m_params.AoSampleCountPerLine);

                //for (int j = 0; j < m_params.AoSampleCountPerLine; j++)
                //{
                //    Y1Wave[offset + j] = m_params.AoY1SamplesPerRow[i];
                //    Y2Wave[offset + j] = m_params.AoY2SamplesPerRow[i];
                //}
            }
        }

        private void Initialize()
        {
            Params m_params = Params.GetParams();

            if (TriggerWave == null || TriggerWave.Length != m_params.DoSampleCountPerLine)
            {
                TriggerWave = new byte[m_params.DoSampleCountPerLine];
            }
            if (XWave == null || XWave.Length != m_params.AoSampleCountPerFrame)
            {
                XWave = new double[m_params.AoSampleCountPerFrame];
            }
            if (Y1Wave == null || Y1Wave.Length != m_params.AoSampleCountPerFrame)
            {
                Y1Wave = new double[m_params.AoSampleCountPerFrame];
            }
            if (Y2Wave == null || Y2Wave.Length != m_params.AoSampleCountPerFrame)
            {
                Y2Wave = new double[m_params.AoSampleCountPerFrame];
            }
        }

        #endregion

    }

}
