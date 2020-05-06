using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace confocal_test
{
    public partial class FormMain : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Init();
            InitLoadControls();
        }

        private void Init()
        {

        }

        private void InitLoadControls()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += version;
            Logger.Info(string.Format("get software version: [{0}]", version));
        }

    }
}
