using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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
                Filter = @"CSVFile(*.csv)|*.csv|All files (*.*)|*.*",
                Multiselect = true
            };
            var @return = dialog.ShowDialog() == DialogResult.OK;
            nameFiles = dialog.FileNames;
            return @return;
        }

        public string Quest
        {
            get => questTextBox.Text;
            set => questTextBox.Text = value;
        }

        public string RightAnswer
        {
            get => Answer0TextBox.Text;
            set => Answer0TextBox.Text = value;
        }

        public string Answer1
        {
            get => Answer1TextBox.Text;
            set => Answer1TextBox.Text = value;
        }

        public string Answer2
        {
            get => Answer2TextBox.Text;
            set => Answer2TextBox.Text = value;
        }

        public string Answer3
        {
            get => Answer3TextBox.Text;
            set => Answer3TextBox.Text = value;
        }

        public void RefreshDataGridView(ICollection<Category> categorys)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Categoria");
            dataTable.Columns.Add("Numero de preguntas");
            foreach (var category in categorys)
            {
                dataTable.Rows.Add(category.Name, category.Questions.Count);
            }

            var cont = 0;
            foreach (var category in categorys)
            {
                cont += category.Questions.Count;
            }

            dataTable.Rows.Add("Total", cont);
            CategoryListDataGridView.DataSource = dataTable;
        }

        private void newQuest_Click(object sender, EventArgs e)
        {
            _controller.NewQuest();
        }

        private void ControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _controller.CloseResquest();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}