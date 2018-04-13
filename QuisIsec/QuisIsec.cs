using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using lib;

namespace QuisIsec
{
    public partial class QuisIsec : MetroFramework.Forms.MetroForm, IView
    {
        private List<Category> _categorys = new List<Category>();


        public Form Form => this;

        public QuisIsec()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameFiles"></param>
        /// <returns>false -> não há ficheiros</returns>
        private bool StartDialog(out string[] nameFiles)
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

        private void QuisIsec_Load(object sender, EventArgs e)
        {
            if (StartDialog(out var nameFiles))
            {
                foreach (var nameFile in nameFiles)
                {
                    var file = new CsvFile(nameFile);
                    if (!file.FileExists() || !file.Open())
                        continue;

                    while (file.HasNextLine())
                    {
                        var line = file.GetNextLine();
                        var cat = line[0];
                        line.RemoveAt(0);
                        var quest = line[0];
                        line.RemoveAt(0);
                        var i = 0;
                        for (; i < _categorys.Count; i++)
                        {
                            if (string.Compare(_categorys[i].Name, cat, StringComparison.Ordinal) == 0)
                                break;
                        }

                        if (i >= _categorys.Count)
                        {
                            _categorys.Add(new Category(cat));
                        }

                        if (line.All(item => item.Any()))
                            _categorys[i].AddQuestion(new Question(quest, line));
                    }

                    _categorys.Sort();
                    file.Close();
                }

                var cont = 0;
                foreach (var category in _categorys)
                {
                    cont += category.Questions.Count;
                }

                preguntaLabel.Text = _categorys[0].Questions[0].Quest;
                resposta_0.Text = _categorys[0].Questions[0].RightAnswer;
                resposta_1.Text = _categorys[0].Questions[0].OthersAnswer[0];
                resposta_2.Text = _categorys[0].Questions[0].OthersAnswer[1];
                resposta_3.Text = _categorys[0].Questions[0].OthersAnswer[2];
            }
        }

        public Color _BackColorQuest { get; set; } = Color.FromArgb(5, 100, 187);
        public Color _BackColorBackPanel { get; set; } = Color.FromArgb(0, 134, 191);
        public Color _BackColorAnswer { get; set; } = Color.FromArgb(5, 100, 187);

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel1, _BackColorBackPanel, e);
        }

        private void preguntaLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(preguntaLabel, _BackColorQuest, e);
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel3, _BackColorAnswer, e);
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel4, _BackColorAnswer, e);
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel5, _BackColorAnswer, e);
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel6, _BackColorAnswer, e);
        }

        private void PaintRoundEdges(Control graphElement, Color color, PaintEventArgs e)
        {
            using (var graphicsPath = _getRoundRectangle(graphElement.ClientRectangle))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(color))
                    e.Graphics.FillPath(brush, graphicsPath);
                using (var pen = new Pen(color, 1.0f))
                    e.Graphics.DrawPath(pen, graphicsPath);
                TextRenderer.DrawText(e.Graphics, graphElement.Text, graphElement.Font, graphElement.ClientRectangle,
                    graphElement.ForeColor);
            }
        }

        private GraphicsPath _getRoundRectangle(Rectangle rectangle)
        {
            var cornerRadius = 80; // change this value according to your needs
            var diminisher = 1;
            var path = new GraphicsPath();
            path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(rectangle.X + rectangle.Width - cornerRadius - diminisher, rectangle.Y, cornerRadius,
                cornerRadius, 270, 90);
            path.AddArc(rectangle.X + rectangle.Width - cornerRadius - diminisher,
                rectangle.Y + rectangle.Height - cornerRadius - diminisher, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - cornerRadius - diminisher, cornerRadius,
                cornerRadius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}