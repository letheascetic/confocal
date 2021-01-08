using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描区域类型
    /// </summary>
    public class ScanAreaTypeModel : ScanPropertyBaseModel
    {
        public static ScanAreaTypeModel Initialize(int id)
        {
            if (id == 0)
            {
                return new ScanAreaTypeModel() { ID = 0, Text = "方形", IsEnabled = true };
            }
            else if (id == 1)
            {
                return new ScanAreaTypeModel() { ID = 1, Text = "矩形", IsEnabled = false };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }

    /// <summary>
    /// 扫描区域
    /// </summary>
    public class ScanAreaModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////




    }
}
