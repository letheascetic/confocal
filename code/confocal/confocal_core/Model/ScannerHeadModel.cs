using confocal_core.Common;
using confocal_core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描头更新事件委托
    /// </summary>
    /// <param name="scannerHead"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScannerHeadModelChangedEventHandler(ScannerHeadModel scannerHead);

    /// <summary>
    /// 扫描头数量：双镜 or 三镜
    /// </summary>
    public class ScannerHeadModel : ScanPropertyBaseModel
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int TWO_SCANNERS = 0;
        public static readonly int THREE_SCANNERS = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static ScannerHeadModel Initialize(int id)
        {
            if (id == TWO_SCANNERS)
            {
                return new ScannerHeadModel() { ID = TWO_SCANNERS, Text = "双镜", IsEnabled = Settings.Default.ScannerHead == TWO_SCANNERS };
            }
            else if (id == THREE_SCANNERS)
            {
                return new ScannerHeadModel() { ID = THREE_SCANNERS, Text = "三镜", IsEnabled = Settings.Default.ScannerHead == THREE_SCANNERS };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }

}
