using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lib;

namespace QuisIsec
{
    public partial class ControlPanel : MetroFramework.Forms.MetroForm, IControlPanelView
    {
        private ControlPanelController _controller;

        public ControlPanel()
        {
            InitializeComponent();
        }

        public void SetController(ControlPanelController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameFiles"></param>
        /// <returns>false -> não há ficheiros</returns>
        public bool RequestNameFiles(out string[] nameFiles)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\C#\QuisIsec\QuisIsec\bin\Debug", //@"C:\",
                Filter = "CSVFile(*.csv)|*.csv|All files (*.*)|*.*",
                Multiselect = true
            };
            var @return = dialog.ShowDialog() == DialogResult.OK;
            nameFiles = dialog.FileNames;
            return @return;
        }
    }
}