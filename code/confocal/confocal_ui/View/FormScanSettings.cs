using C1.Win.C1Ribbon;
using confocal_ui.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_ui
{
    public partial class FormScanSettings : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        private ScanSettingsViewModel mScanSettingsVM;


        public FormScanSettings()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            mScanSettingsVM = new ScanSettingsViewModel();
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormScanSettings_Load(object sender, EventArgs e)
        {

        }

    }
}
