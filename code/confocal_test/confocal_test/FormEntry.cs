using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace confocal_test
{
    public partial class FormEntry : Form
    {
        [DllImport("core.dll", EntryPoint = "Test2", CallingConvention = CallingConvention.Cdecl)]
        static extern int Test2(int val);

        [DllImport("core.dll", EntryPoint = "Config::Test", CallingConvention = CallingConvention.Cdecl)]
        static extern int Test(int val);

        public FormEntry()
        {
            InitializeComponent();

            int val = Test2(6);

            val = Test(6);

        }
    }
}
