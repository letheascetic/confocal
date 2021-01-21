using confocal_core.Common;
using confocal_core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描模式更新事件委托
    /// </summary>
    /// <param name="scanMode"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanModeChangedEventHandler(ScanModeModel scanMode);

    /// <summary>
    /// 扫描模式：Galvano 或 Resonant
    /// </summary>
    public class ScanModeModel : ScanPropertyBaseModel
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int RESONANT = 0;
        public static readonly int GALVANO = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static ScanModeModel Initialize(int id)
        {
            if (id == RESONANT)
            {
                return new ScanModeModel() { ID = RESONANT, Text = "Resonant", IsEnabled = Settings.Default.ScanMode == RESONANT };
            }
            else if (id == GALVANO)
            {
                return new ScanModeModel() { ID = GALVANO, Text = "Galvano", IsEnabled = Settings.Default.ScanMode == GALVANO };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }

}
