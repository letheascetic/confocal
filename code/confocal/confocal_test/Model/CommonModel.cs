using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_test.Model
{
    public class CommonModel : ObservableObject
    {
        private volatile static CommonModel pCommon = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public static CommonModel GetCommonModel()
        {
            lock (locker)
            {
                if (pCommon == null)
                {
                    pCommon = new CommonModel();
                }
            }
            return pCommon;
        }

        private CommonModel()
        {
            ID = 0;
        }

    }
}
