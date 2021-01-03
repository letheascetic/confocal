using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
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
        public static ScanAcquisitionModel Initialize(int id)
        {
            if (id == 0)
            {
                return new ScanAcquisitionModel() { ID = 0, IsEnabled = false, Text = "实时" };
            }
            else if (id == 1)
            {
                return new ScanAcquisitionModel() { ID = 1, IsEnabled = false, Text = "捕捉" };
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
                return new ScanDirectionModel() { ID = UNIDIRECTION, Text = "单向", IsEnabled = true };
            }
            else if (id == BIDIRECTION)
            {
                return new ScanDirectionModel() { ID = BIDIRECTION, Text = "双向", IsEnabled = false };
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
                return new ScanModeModel() { ID = RESONANT, Text = "Resonant", IsEnabled = false };
            }
            else if (id == GALVANO)
            {
                return new ScanModeModel() { ID = GALVANO, Text = "Galvano", IsEnabled = true };
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
                return new ScannerHeadModel() { ID = TWO_SCANNERS, Text = "双镜", IsEnabled = true };
            }
            else if (id == THREE_SCANNERS)
            {
                return new ScannerHeadModel() { ID = THREE_SCANNERS, Text = "三镜", IsEnabled = false };
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
                new ScanPixelModel(){ ID = 0, IsEnabled = false, Text = "64", Data = 64 },
                new ScanPixelModel(){ ID = 1, IsEnabled = false, Text = "128", Data = 128 },
                new ScanPixelModel(){ ID = 2, IsEnabled = false, Text = "256", Data = 256 },
                new ScanPixelModel(){ ID = 3, IsEnabled = true, Text = "512", Data = 512 },
                new ScanPixelModel(){ ID = 4, IsEnabled = false, Text = "1024", Data = 1024 },
                new ScanPixelModel(){ ID = 5, IsEnabled = false, Text = "2048", Data = 2048 },
                new ScanPixelModel(){ ID = 6, IsEnabled = false, Text = "4096", Data = 4096 }
            };
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
                new ScanPixelDwellModel(){ ID = 0, IsEnabled = false, Text = "2", Data = 2 },
                new ScanPixelDwellModel(){ ID = 1, IsEnabled = false, Text = "4", Data = 4 },
                new ScanPixelDwellModel(){ ID = 2, IsEnabled = false, Text = "6", Data = 6 },
                new ScanPixelDwellModel(){ ID = 3, IsEnabled = true, Text = "8", Data = 8 },
                new ScanPixelDwellModel(){ ID = 4, IsEnabled = false, Text = "10", Data = 10 },
                new ScanPixelDwellModel(){ ID = 5, IsEnabled = false, Text = "20", Data = 20 },
                new ScanPixelDwellModel(){ ID = 6, IsEnabled = false, Text = "50", Data = 50 },
                new ScanPixelDwellModel(){ ID = 7, IsEnabled = false, Text = "100", Data = 100 }
            };
        }
    }

}
