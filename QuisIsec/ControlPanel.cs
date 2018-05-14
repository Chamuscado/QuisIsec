using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            _controller.TeamColorChanged(0, Team0ColorPicker.ColorInitial);
            _controller.TeamColorChanged(1, Team1ColorPicker.ColorInitial);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameFiles"></param>
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
            get => teamPoints0TextBox.Text;
            set => teamPoints0TextBox.Text = value;
        }

        public string Team1Points
        {
            get => teamPoints1TextBox.Text;
            set => teamPoints1TextBox.Text = value;
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

            var cont = categorys.Sum(category => category.Questions.Count);

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

        private void Team0ColorPicker_ColorChanged(int eventNumber)
        {
            _controller.TeamColorChanged(0, Team0ColorPicker.ColorInitial);
        }

        private void Team1ColorPicker_ColorChanged(int eventNumber)
        {
            _controller.TeamColorChanged(1, Team1ColorPicker.ColorInitial);
        }

        private void teamPoints0TextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Team0Points, out var points))
                _controller.TeamPointsChanged(0, points);
        }

        private void teamPoints1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Team1Points, out var points))
                _controller.TeamPointsChanged(1, points);
        }

        private void loadFilesButton_Click(object sender, EventArgs e)
        {
            _controller.LoadFiles();
        }

        private void startGameWinButton_Click(object sender, EventArgs e)
        {
            _controller.StartGameWin();
        }

        #region Answer Select

        private void Equipa0NenhumaResposta_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa0NenhumaResposta.Checked)
                _controller.SetAnswer(0, Answer.None);
        }

        private void Equipa0RespostaA_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa0RespostaA.Checked)
                _controller.SetAnswer(0, Answer.A);
        }

        private void Equipa0RespostaB_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa0RespostaB.Checked)
                _controller.SetAnswer(0, Answer.B);
        }

        private void Equipa0RespostaC_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa0RespostaC.Checked)
                _controller.SetAnswer(0, Answer.C);
        }

        private void Equipa0RespostaD_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa0RespostaD.Checked)
                _controller.SetAnswer(0, Answer.D);
        }

        private void Equipa1NenhumaResposta_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa1NenhumaResposta.Checked)
                _controller.SetAnswer(1, Answer.None);
        }

        private void Equipa1RespostaA_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa1RespostaA.Checked)
                _controller.SetAnswer(1, Answer.A);
        }

        private void Equipa1RespostaB_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa1RespostaB.Checked)
                _controller.SetAnswer(1, Answer.B);
        }

        private void Equipa1RespostaC_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa1RespostaC.Checked)
                _controller.SetAnswer(1, Answer.C);
        }

        private void Equipa1RespostaD_CheckedChanged(object sender, EventArgs e)
        {
            if (Equipa1RespostaD.Checked)
                _controller.SetAnswer(1, Answer.D);
        }

        #endregion

        private void resetTimerButton_Click(object sender, EventArgs e)
        {
            _controller.RestTimer();
        }

        private void startTimerButton_Click(object sender, EventArgs e)
        {
            _controller.StartTimer();
        }

        private void stopTimerButton_Click(object sender, EventArgs e)
        {
            _controller.StopTimer();
        }

        private void buttonShowRightAnswer_Click(object sender, EventArgs e)
        {
            _controller.ShowRightAnswer();
        }
    }
}