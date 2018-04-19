﻿using System;
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
        /// <param name="path"></param>
        /// <returns>false -> não há ficheiros</returns>
        public bool RequestNameFiles(out string[] nameFiles)
        {
            bool @return;
            using (var dialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\João Leitão\Google Drive\Ética e Deontologia\QuISEC", //@"C:\",
                Filter = @"CSVFile(*.csv)|*.csv|All files (*.*)|*.*",
                Multiselect = true
            })
            {
                @return = dialog.ShowDialog() == DialogResult.OK;
                nameFiles = dialog.FileNames;
            }

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

        public string Category
        {
            get => CategoryTextBox.Text;
            set => CategoryTextBox.Text = value;
        }

        public string TeamName1
        {
            get => teamName1.Text;
            set => teamName1.Text = value;
        }

        public string TeamName0
        {
            get => teamName0.Text;
            set => teamName0.Text = value;
        }

        public string Team0Points
        {
            get => pontosEquipa0TextBox.Text;
            set => pontosEquipa0TextBox.Text = value;
        }

        public string Team1Points
        {
            get => pontosEquipa1TextBox.Text;
            set => pontosEquipa1TextBox.Text = value;
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

        private void toGameWindow_Click(object sender, EventArgs e)
        {
            _controller.QuestToGameWindow();
        }

        private void teamName1_TextChanged(object sender, EventArgs e)
        {
            _controller.TeamNameChanged(1, TeamName1);
        }


        private void teamName0_TextChanged(object sender, EventArgs e)
        {
            _controller.TeamNameChanged(0, TeamName0);
        }

        private void Equipa0ColorPicker_ColorChanged(int eventNumber)
        {
            _controller.TeamColorChanged(0, Equipa0ColorPicker.ColorInitial);
        }

        private void Equipa1ColorPicker_ColorChanged(int eventNumber)
        {
            _controller.TeamColorChanged(1, Equipa1ColorPicker.ColorInitial);
        }

        private void pontosEquipa0TextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Team0Points, out var points))
                _controller.TeamPointsChanged(0, points);
            else
            {
                MessageBox.Show(@"Apenas numeros!!!");
            }
        }

        private void pontosEquipa1TextBox_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Team1Points, out var points))
                _controller.TeamPointsChanged(1, points);
            else
            {
                MessageBox.Show(@"Apenas numeros!!!");
            }
        }

        private void loadFilesButton_Click(object sender, EventArgs e)
        {
            _controller.LoadFiles();
        }

        private void startGameWinButton_Click(object sender, EventArgs e)
        {
            _controller.StartGameWin();
        }
    }
}