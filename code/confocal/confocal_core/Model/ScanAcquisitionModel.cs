using confocal_core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 采集模式更新事件委托
    /// </summary>
    /// <param name="scanAcquisition"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanAcquisitionChangedEventHandler(ScanAcquisitionModel scanAcquisition);

    /// <summary>
    /// 采集模式：实时、捕捉
    /// </summary>
    public class ScanAcquisitionModel : ScanPropertyBaseModel
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int LIVE = 0;
        public static readonly int CAPTURE = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static ScanAcquisitionModel Initialize(int id)
        {
            if (id == LIVE)
            {
                return new ScanAcquisitionModel() { ID = LIVE, IsEnabled = false, Text = "实时" };
            }
            else if (id == CAPTURE)
            {
                return new ScanAcquisitionModel() { ID = CAPTURE, IsEnabled = false, Text = "捕捉" };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }
}
