using confocal_core.Common;
using confocal_core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描像素更新事件委托
    /// </summary>
    /// <param name="scanPixel"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanPixelChangedEventHandler(ScanPixelModel scanPixel);

    /// <summary>
    /// 扫描像素
    /// </summary>
    public class ScanPixelModel : ScanPropertyWithValueBaseModel<int>
    {
        public static List<ScanPixelModel> Initialize()
        {
            return new List<ScanPixelModel>()
            {
                new ScanPixelModel(){ ID = 0, IsEnabled = Settings.Default.ScanPixel == 0, Text = "64", Data = 64 },
                new ScanPixelModel(){ ID = 1, IsEnabled = Settings.Default.ScanPixel == 1, Text = "128", Data = 128 },
                new ScanPixelModel(){ ID = 2, IsEnabled = Settings.Default.ScanPixel == 2, Text = "256", Data = 256 },
                new ScanPixelModel(){ ID = 3, IsEnabled = Settings.Default.ScanPixel == 3, Text = "512", Data = 512 },
                new ScanPixelModel(){ ID = 4, IsEnabled = Settings.Default.ScanPixel == 4, Text = "1024", Data = 1024 },
                new ScanPixelModel(){ ID = 5, IsEnabled = Settings.Default.ScanPixel == 5, Text = "2048", Data = 2048 },
                new ScanPixelModel(){ ID = 6, IsEnabled = Settings.Default.ScanPixel == 6, Text = "4096", Data = 4096 }
            };
        }

        public static ScanPixelModel Initialize(int id)
        {
            return new ScanPixelModel() { ID = id, IsEnabled = Settings.Default.ScanPixel == id, Text = ((int)Math.Pow(2, Settings.Default.ScanPixel) * 64).ToString(), Data = (int)Math.Pow(2, Settings.Default.ScanPixel) * 64 };
        }
    }
}
