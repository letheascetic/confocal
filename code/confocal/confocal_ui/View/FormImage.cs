using C1.Win.C1Ribbon;
using confocal_core.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_ui.View
{
    public partial class FormImage : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormImage()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            ushort[] data = new ushort[2 * 10 * 5];
            for (int i = 0; i < data.Length; i++)
            {
                //int j = i / 8;
                data[i] = (ushort)i;
            }
            Matrix.ToMatrix(data, 2, 10, 5, 0);
        }
    }
}
