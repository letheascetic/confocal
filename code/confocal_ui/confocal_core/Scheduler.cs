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
        private Params m_params;
        private Config m_config;
        private Waver m_waver;
        private NiCard m_card;
        private ScanTask m_scanTask;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public ScanTask CurrentScanTask { get { return m_scanTask; } }
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

        public API_RETURN_CODE StartScanTask()
        {
            m_params.Calculate();                       // 计算参数
            m_waver.Generate();                         // 计算AO输出波形和触发信号

            m_scanTask = new ScanTask();                // 新建扫描任务
            m_scanTask.Config();                        // 配置扫描任务

            API_RETURN_CODE code = m_card.Start();      // 启动板卡
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Logger.Info(string.Format("start to scan failed: [{0}].", code));
                return code;
            }

            m_scanTask.Start();                         // 启动扫描任务

            Logger.Info(string.Format("start scan task success."));
            return code;
        }

        public void StopScanTask()
        {
            m_card.Stop();
            m_scanTask.Stop();
            Logger.Info(string.Format("stop scan task."));
        }

        #endregion

        #region private apis    

        private Scheduler()
        {
            m_params = Params.GetParams();
            m_config = Config.GetConfig();
            m_waver = Waver.GetWaver();
            m_card = NiCard.CreateInstance();
            m_scanTask = null;
            Init();
        }

        private void Init()
        {
            m_params.Calculate();
        }

        #endregion
    }
}
