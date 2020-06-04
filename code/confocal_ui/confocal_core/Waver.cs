using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Params m_params = Params.GetParams();

            if (TriggerWave == null || TriggerWave.Length != m_params.SampleCountPerLine)
            {
                TriggerWave = new byte[m_params.SampleCountPerLine];
            }
            if (XWave == null || XWave.Length != m_params.SampleCountPerFrame)
            {
                XWave = new double[m_params.SampleCountPerFrame];
            }
            if (Y1Wave == null || Y1Wave.Length != m_params.SampleCountPerFrame)
            {
                Y1Wave = new double[m_params.SampleCountPerFrame];
            }
            if (Y2Wave == null || Y2Wave.Length != m_params.SampleCountPerFrame)
            {
                Y2Wave = new double[m_params.SampleCountPerFrame];
            }

            Array.Copy(m_params.DigitalTriggerSamplesPerLine, TriggerWave, m_params.SampleCountPerLine);

            int offset = -m_params.SampleCountPerLine;
            for (int i = 0; i < Config.GetConfig().GetScanYPoints(); i++)
            {
                offset += m_params.SampleCountPerLine;
                Array.Copy(m_params.XSamplesPerLine, 0, XWave, offset, m_params.SampleCountPerLine);

                for (int j = 0; j < m_params.SampleCountPerLine; j++)
                {

                    Y1Wave[offset + j] = m_params.Y1SamplesPerRow[i];
                    Y2Wave[offset + j] = m_params.Y2SamplesPerRow[i];
                }
            }
        }

        private Waver()
        {

        }

        #endregion

    }

}
