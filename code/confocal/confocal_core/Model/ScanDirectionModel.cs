using confocal_core.Common;
using confocal_core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描方向更新事件委托
    /// </summary>
    /// <param name="scanDirection"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanDirectionChangedEventHandler(ScanDirectionModel scanDirection);

    /// <summary>
    /// 扫描方向：单向、双向
    /// </summary>
    public class ScanDirectionModel : ScanPropertyBaseModel
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int UNIDIRECTION = 0;
        public static readonly int BIDIRECTION = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static ScanDirectionModel Initialize(int id)
        {
            if (id == UNIDIRECTION)
            {
                return new ScanDirectionModel() { ID = UNIDIRECTION, Text = "单向", IsEnabled = Settings.Default.ScanDirection == UNIDIRECTION };
            }
            else if (id == BIDIRECTION)
            {
                return new ScanDirectionModel() { ID = BIDIRECTION, Text = "双向", IsEnabled = Settings.Default.ScanDirection == BIDIRECTION };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }

}
