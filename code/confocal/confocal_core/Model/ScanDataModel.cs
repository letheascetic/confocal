using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class ScanDataModel : ObservableObject
    {
        private ScanImageModel[] mOriginIamges;
        private ScanImageModel[] mGrayImages;
        private ScanImageModel[] mGray3Images;
        private ScanImageModel[] mBGRImages;

        /// <summary>
        /// 原始图像
        /// </summary>
        public ScanImageModel[] OriginImages
        {
            get { return mOriginIamges; }
            set { mOriginIamges = value; RaisePropertyChanged(() => OriginImages); }
        }
        /// <summary>
        /// 灰度图像
        /// </summary>
        public ScanImageModel[] GrayImages
        {
            get { return mGrayImages; }
            set { mGrayImages = value; RaisePropertyChanged(() => GrayImages); }
        }
        /// <summary>
        /// 三通道灰度图像
        /// </summary>
        public ScanImageModel[] Gray3Images
        {
            get { return mGray3Images; }
            set { mGray3Images = value; RaisePropertyChanged(() => Gray3Images); }
        }
        /// <summary>
        /// BGR彩色图像
        /// </summary>
        public ScanImageModel[] BGRImages
        {
            get { return mBGRImages; }
            set { mBGRImages = value; }
        }

        public ScanDataModel(int rows, int columns, int numOfBank, int numOfChannels, bool[] statusOfChannels)
        {
            OriginImages = new ScanImageModel[numOfChannels];
            GrayImages = new ScanImageModel[numOfChannels];
            Gray3Images = new ScanImageModel[numOfChannels];
            BGRImages = new ScanImageModel[numOfChannels];
            for (int i = 0; i < statusOfChannels.Length; i++)
            {
                if (statusOfChannels[i])
                {
                    OriginImages[i] = new ScanImageModel(rows, columns, Emgu.CV.CvEnum.DepthType.Cv32S, 1, numOfBank);
                    GrayImages[i] = new ScanImageModel(rows, columns, Emgu.CV.CvEnum.DepthType.Cv8U, 1, numOfBank);
                    Gray3Images[i] = new ScanImageModel(rows, columns, Emgu.CV.CvEnum.DepthType.Cv8U, 3, numOfBank);
                    BGRImages[i] = new ScanImageModel(rows, columns, Emgu.CV.CvEnum.DepthType.Cv8U, 3, numOfBank);
                }
                else
                {
                    OriginImages[i] = null;
                    GrayImages[i] = null;
                    Gray3Images[i] = null;
                    BGRImages[i] = null;
                }
            }
        }
    
    }
}
