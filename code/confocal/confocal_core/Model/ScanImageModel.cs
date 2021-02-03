using confocal_core.Common;
using Emgu.CV;
using Emgu.CV.CvEnum;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public delegate API_RETURN_CODE ScanImageUpdatedEventHandler(ScanImageModel[] scanImages);

    /// <summary>
    /// 扫描图像
    /// </summary>
    public class ScanImageModel : ObservableObject
    {
        private int numOfBank;
        private Mat matImage;
        private ScanBankModel[] banks;

        /// <summary>
        /// 扫描图像
        /// </summary>
        public Mat Image
        {
            get { return matImage; }
            set { matImage = value; RaisePropertyChanged(() => Image); }
        }
        /// <summary>
        /// 扫描图像块
        /// </summary>
        public ScanBankModel[] Banks
        {
            get { return banks; }
            set { banks = value; RaisePropertyChanged(() => Banks); }
        }
        /// <summary>
        /// 扫描图像块的数量
        /// </summary>
        public int NumOfBank
        {
            get { return numOfBank; }
            set { numOfBank = value; RaisePropertyChanged(() => NumOfBank); }
        }

        public ScanImageModel(int rows, int columns, DepthType type, int channels, int numOfBank)
        {
            if (rows % numOfBank != 0)
            {
                throw new ArgumentException(string.Format("Rows[{0}] % NumOfBank[{1}] != 0", rows, numOfBank));
            }
            
            NumOfBank = numOfBank;
            Image = new Mat(rows, columns, type, channels);

            int rowIndex;
            int rowsOfBank = rows / NumOfBank;
            Banks = new ScanBankModel[NumOfBank];
            for (int i = 0; i < NumOfBank; i++)
            {
                rowIndex = i * rowsOfBank;
                Banks[i] = new ScanBankModel(rowsOfBank, columns, type, channels, Image.Row(rowIndex).DataPointer, Image.Step, i);
            }
        }

        public void Dispose()
        {
            Image.Dispose();
        }

    }

    /// <summary>
    /// 图像块
    /// </summary>
    public class ScanBankModel : ObservableObject
    {
        private int index;
        private Mat bank;

        /// <summary>
        /// 子图像数据
        /// </summary>
        public Mat Bank
        {
            get { return bank; }
            set { bank = value; RaisePropertyChanged(() => Bank); }
        }
        /// <summary>
        /// 子图像在图像中的索引号
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; RaisePropertyChanged(() => Index); }
        }

        public ScanBankModel(int rows, int columns, DepthType type, int channels, IntPtr data, int step, int index)
        {
            Bank = new Mat(rows, columns, type, channels, data, step);
            Index = index;
        }

    }
}
