using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_core
{
    /// <summary>
    /// 调度器，单例模式
    /// </summary>
    public class Scheduler
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Scheduler pScheduler = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private bool m_scanning;
        private Params m_params;
        private Config m_config;
        private Waver m_waver;
        private NiCard m_card;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Scanning { get { return m_scanning; } }
        ///////////////////////////////////////////////////////////////////////////////////////////
        #region public apis

        public static Scheduler CreateInstance()
        {
            if (pScheduler == null)
            {
                lock (locker)
                {
                    if (pScheduler == null)
                    {
                        pScheduler = new Scheduler();
                    }
                }
            }
            return pScheduler;
        }

        public void StartToScan()
        {
            m_params.Calculate();
            m_waver.Generate();
            if (m_card.Start() == API_RETURN_CODE.API_SUCCESS)
            {
                m_scanning = true;
                Logger.Info(string.Format("start to scan success."));
            }
        }

        public void Stop()
        {
            m_card.Stop();
            m_scanning = false;
        }

        #endregion

        #region private apis    

        private Scheduler()
        {
            m_params = Params.GetParams();
            m_config = Config.GetConfig();
            m_waver = Waver.GetWaver();
            m_card = NiCard.CreateInstance();

            m_scanning = false;

            m_params.Calculate();
        }

        #endregion
    }
}
