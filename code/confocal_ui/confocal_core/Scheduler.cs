using Emgu.CV;
using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Windows.Forms;

namespace confocal_core
{
    public delegate API_RETURN_CODE ScanTaskEventHandler(ScanTask scanTask, Object paras);
    public delegate API_RETURN_CODE ChannelEventHandler(ScanTask scanTask, CHAN_ID id, Object paras);

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
        public static readonly int REAL_TIME_SCAN_TASK_ID = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public event ScanTaskEventHandler ScanTaskCreated;
        public event ScanTaskEventHandler ScanTaskStarted;
        public event ScanTaskEventHandler ScanTaskStopped;
        public event ScanTaskEventHandler ScanTaskReleased;
        public event ScanTaskEventHandler ActivatedChannelChanged;
        public event ScanTaskEventHandler ScanTaskConfigured;
        public event ChannelEventHandler SelectedChannelChanged;
        public event ChannelEventHandler ChannelColorReferenceChanged;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Params m_params;
        private Config m_config;
        private Waver m_waver;
        private NiCard m_card;
        private List<ScanTask> m_scanTasks;     // 扫描任务集
        private ScanTask m_scanningTask;        // 当前在扫描的任务
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        #region public apis

        public bool TaskScanning()
        {
            if (m_scanningTask == null)
            {
                return false;
            }
            return m_scanningTask.Scannning;
        }

        public ScanTask GetScanningTask()
        {
            return m_scanningTask;
        }

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

        public API_RETURN_CODE CreateScanTask(int taskId, string taskName, out ScanTask scanTask)
        {
            scanTask = FindScanTask(taskId);
            if (scanTask == null)
            {
                scanTask = new ScanTask(taskId, taskName);
                m_scanTasks.Add(scanTask);
            }

            if (ScanTaskCreated != null)
            {
                ScanTaskCreated.Invoke(scanTask, null);
            }

            Logger.Info(string.Format("create scan task[{0}|{1}].", taskId, taskName));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE StartScanTask(ScanTask scanTask)
        {
            if(scanTask == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_INVALID;
            }

            if (FindScanTask(scanTask.TaskId) == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_NOT_FOUND;
            }

            m_params.Calculate();                       // 计算参数
            m_params.GenerateColorMapping();            // 
            m_params.GenerateAiChannelIndex();          // 计算使用PMT的情况下各个AI通道采集数据对应的Index
            m_waver.Generate();                         // 计算AO输出波形和触发信号
            scanTask.Config();                          // 配置扫描任务

            API_RETURN_CODE code = m_card.Start();      // 启动板卡
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Logger.Info(string.Format("start scan task[{0}|{1}] failed: [{2}].", scanTask.TaskId, scanTask.TaskName, code));
                StopScanTask(scanTask);
                return code;
            }

            scanTask.Start();                           // 启动扫描任务
            m_scanningTask = scanTask;                  

            if (ScanTaskStarted != null)
            {
                ScanTaskStarted.Invoke(scanTask, null);
            }

            Logger.Info(string.Format("start scan task[{0}|{1}] success.", scanTask.TaskId, scanTask.TaskName));
            return code;
        }

        public API_RETURN_CODE StopScanTask(ScanTask scanTask)
        {
            if (scanTask == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_INVALID;
            }

            if (FindScanTask(scanTask.TaskId) == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_NOT_FOUND;
            }

            m_card.Stop();
            scanTask.Stop();
            m_scanningTask = null;

            if (ScanTaskStopped != null)
            {
                ScanTaskStopped.Invoke(scanTask, null);
            }

            Logger.Info(string.Format("stop scan task[{0}|{1}].", scanTask.TaskId, scanTask.TaskName));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ReleaseScanTask(ScanTask scanTask)
        {
            API_RETURN_CODE code = StopScanTask(scanTask);
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                return code;
            }

            m_scanTasks.Remove(scanTask);

            if (ScanTaskReleased != null)
            {
                ScanTaskReleased.Invoke(scanTask, null);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ChangeActivatedChannels(ScanTask scanTask)
        {
            if (scanTask == null || !scanTask.Scannning)
            {
                return API_RETURN_CODE.API_SUCCESS;
            }

            if (FindScanTask(scanTask.TaskId) == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_NOT_FOUND;
            }

            if (ActivatedChannelChanged != null)
            {
                ActivatedChannelChanged.Invoke(scanTask, null);
            }

            Logger.Info(string.Format("change activated channels[{0}|{1}].", scanTask.TaskId, scanTask.TaskName));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ConfigScanTask(ScanTask scanTask)
        {
            m_params.Calculate();                       // 计算参数

            if (ScanTaskConfigured != null)
            {
                ScanTaskConfigured.Invoke(scanTask, null);
            }

            Logger.Info(string.Format("config scan task."));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ChangeSelectedChannel(ScanTask scanTask, CHAN_ID id)
        {
            if (SelectedChannelChanged != null)
            {
                SelectedChannelChanged.Invoke(scanTask, id, null);
            }

            Logger.Info(string.Format("change selected channel[{0}].", id));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ChangeChannelColorReference(ScanTask scanTask, CHAN_ID id, Color color)
        {
            m_config.SetChannelColorReference(id, color);
            m_params.GenerateColorMapping(id);
            Mat mapping = m_params.ColorMappingMat[(int)id];
            scanTask.GetScanData().ScanImage.UpdateDisplayImage((int)id, mapping);

            if (ChannelColorReferenceChanged != null)
            {
                ChannelColorReferenceChanged.Invoke(scanTask, id, color);
            }

            Logger.Info(string.Format("change channel[{0}] color reference[{1}].", id, color));
            return API_RETURN_CODE.API_SUCCESS;
        }

        #endregion

        #region private apis    

        private Scheduler()
        {
            Init();
        }

        private void Init()
        {
            m_params = Params.GetParams();
            m_config = Config.GetConfig();
            m_waver = Waver.GetWaver();
            m_card = NiCard.CreateInstance();
            m_scanTasks = new List<ScanTask>();
            m_scanningTask = null;
            m_params.Calculate();
        }

        private ScanTask FindScanTask(int taskId)
        {
            foreach (ScanTask scanTask in m_scanTasks)
            {
                if (scanTask.TaskId == taskId)
                {
                    return scanTask;
                }
            }
            return null;
        }

        #endregion
    }
}
