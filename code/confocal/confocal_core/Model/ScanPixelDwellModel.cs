using confocal_core.Common;
using confocal_core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{    
    /// <summary>
     /// 扫描像素时间更新事件委托
     /// </summary>
     /// <param name="scanPixelDwell"></param>
     /// <returns></returns>
    public delegate API_RETURN_CODE ScanPixelDwellChangedEventHandler(ScanPixelDwellModel scanPixelDwell);

    /// <summary>
    /// 扫描像素停留时间
    /// </summary>
    public class ScanPixelDwellModel : ScanPropertyWithValueBaseModel<int>
    {
        public static List<ScanPixelDwellModel> Initialize()
        {
            return new List<ScanPixelDwellModel>()
            {
                new ScanPixelDwellModel(){ ID = 0, IsEnabled = Settings.Default.ScanPixelDwell == 0, Text = "2", Data = 2 },
                new ScanPixelDwellModel(){ ID = 1, IsEnabled = Settings.Default.ScanPixelDwell == 1, Text = "4", Data = 4 },
                new ScanPixelDwellModel(){ ID = 2, IsEnabled = Settings.Default.ScanPixelDwell == 2, Text = "6", Data = 6 },
                new ScanPixelDwellModel(){ ID = 3, IsEnabled = Settings.Default.ScanPixelDwell == 3, Text = "8", Data = 8 },
                new ScanPixelDwellModel(){ ID = 4, IsEnabled = Settings.Default.ScanPixelDwell == 4, Text = "10", Data = 10 },
                new ScanPixelDwellModel(){ ID = 5, IsEnabled = Settings.Default.ScanPixelDwell == 5, Text = "20", Data = 20 },
                new ScanPixelDwellModel(){ ID = 6, IsEnabled = Settings.Default.ScanPixelDwell == 6, Text = "50", Data = 50 },
                new ScanPixelDwellModel(){ ID = 7, IsEnabled = Settings.Default.ScanPixelDwell == 7, Text = "100", Data = 100 }
            };
        }

        public static ScanPixelDwellModel Initialize(int id)
        {
            return Initialize().Where(p => p.ID == id).First();
        }

    }

}
