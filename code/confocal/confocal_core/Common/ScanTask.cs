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
        private static bool m_scanning = false;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private int mTaskId;
        private string mTaskName;

        public int TaskId
        {
            get { return mTaskId; }
            set { mTaskId = value; }
        }

        public string TaskName
        {
            get { return mTaskName; }
            set { mTaskName = value; }
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
