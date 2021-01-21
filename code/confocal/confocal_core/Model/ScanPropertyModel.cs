using confocal_core.Common;
using confocal_core.Properties;
using GalaSoft.MvvmLight;
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
    /// 扫描头更新事件委托
    /// </summary>
    /// <param name="scannerHead"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScannerHeadModelChangedEventHandler(ScannerHeadModel scannerHead);
    /// <summary>
    /// 扫描方向更新事件委托
    /// </summary>
    /// <param name="scanDirection"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanDirectionChangedEventHandler(ScanDirectionModel scanDirection);
    /// <summary>
    /// 扫描模式更新事件委托
    /// </summary>
    /// <param name="scanMode"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanModeChangedEventHandler(ScanModeModel scanMode);
    /// <summary>
    /// 跳行扫描使能更新事件委托
    /// </summary>
    /// <param name="lineSkipEnabled"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE LineSkipEnableChangedEventHandler(bool lineSkipEnabled);
    /// <summary>
    /// 跳行扫描参数更新事件委托
    /// </summary>
    /// <param name="lineSkip"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE LineSkipChangedEventHandler(ScanLineSkipModel lineSkip);
    /// <summary>
    /// 扫描像素更新事件委托
    /// </summary>
    /// <param name="scanPixel"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanPixelChangedEventHandler(ScanPixelModel scanPixel);
    /// <summary>
    /// 扫描像素时间更新事件委托
    /// </summary>
    /// <param name="scanPixelDwell"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanPixelDwellChangedEventHandler(ScanPixelDwellModel scanPixelDwell);

    public class ScanPropertyBaseModel : ObservableObject
    {
        private int id;
        private string text;
        private bool isEnabled;

        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; RaisePropertyChanged(() => IsEnabled); }
        }
    }

    public class ScanPropertyWithValueBaseModel<T> : ScanPropertyBaseModel
    {
        private T data;

        public T Data
        {
            get { return data; }
            set { this.data = value; RaisePropertyChanged(() => Data); }
        }
    }

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
