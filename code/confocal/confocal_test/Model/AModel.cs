using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_test.Model
{
    public class AModel : ObservableObject
    {
        private volatile static AModel pModel = null;
        private static readonly object locker = new object();

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public static AModel GetAModel()
        {
            lock (locker)
            {
                if (pModel == null)
                {
                    pModel = new AModel();
                }
            }
            return pModel;
        }

        private AModel()
        {
            ID = CommonModel.GetCommonModel().ID;
        }

    }
}
