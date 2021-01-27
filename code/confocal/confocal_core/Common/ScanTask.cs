using confocal_core.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    public class ScanTask
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private int mTaskId;
        private string mTaskName;
        private ScanInfoModel mScanInfo;
        private ScanDataModel mScanData;

        /// <summary>
        /// 扫描任务ID
        /// </summary>
        public int TaskId
        {
            get { return mTaskId; }
            set { mTaskId = value; }
        }
        /// <summary>
        /// 扫描任务名
        /// </summary>
        public string TaskName
        {
            get { return mTaskName; }
            set { mTaskName = value; }
        }
        /// <summary>
        /// 扫描信息
        /// </summary>
        public ScanInfoModel ScanInfo
        {
            get { return mScanInfo; }
            set { mScanInfo = value; }
        }

        public ScanDataModel ScanData
        {
            get { return mScanData; }
            set { mScanData = value; }
        }

        public ScanTask(int taskId, string taskName)
        {
            TaskId = taskId;
            TaskName = taskName;
        }

        public void Start()
        {
            
        }

        public void Stop()
        {

        }

    }
}
