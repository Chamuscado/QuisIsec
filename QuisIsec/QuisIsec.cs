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
    public partial class QuisIsec : MetroFramework.Forms.MetroForm, IGameView
    {
        private GameViewController _controller;

        public void SetController(GameViewController controller)
        {
            _controller = controller;
        }

        public string Quest
        {
            get => preguntaLabel.Text;
            set => preguntaLabel.Text = value;
        }

        public string Answer0
        {
            get => resposta_0.Text;
            set => resposta_0.Text = value;
        }

        public string Answer1
        {
            get => resposta_1.Text;
            set => resposta_1.Text = value;
        }

        public string Answer2
        {
            get => resposta_2.Text;
            set => resposta_2.Text = value;
        }

        public string Answer3
        {
            get => resposta_3.Text;
            set => resposta_3.Text = value;
        }

        public string Team0Name
        {
            get => team0Name.Text;
            set => team0Name.Text = value;
        }

        public string Team1Name
        {
            get => team1Name.Text;
            set => team1Name.Text = value;
        }

        public int Team0Points
        {
            get => int.Parse(team0Points.Text);
            set => team0Points.Text = value.ToString();
        }

        public int Team1Points
        {
            get => int.Parse(team1Points.Text);
            set => team1Points.Text = value.ToString();
        }

        public Form Form => this;

        public QuisIsec()
        {
            InitializeComponent();
        }

        private readonly Color _backColorQuest = Color.FromArgb(5, 100, 187);
        private readonly Color _backColorBackPanel = Color.FromArgb(0, 134, 191);
        private readonly Color _backColorAnswer = Color.FromArgb(5, 100, 187);
        private readonly Color _backColorPointsPanel = Color.FromArgb(0, 134, 191);

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel1, _backColorBackPanel, e);
        }

        private void preguntaLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(preguntaLabel, _backColorQuest, e);
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel3, _backColorAnswer, e);
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel4, _backColorAnswer, e);
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel5, _backColorAnswer, e);
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel6, _backColorAnswer, e);
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
            const int cornerRadius = 40; // change this value according to your needs
            const int diminisher = 1;
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

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel8, _backColorBackPanel, e);
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel9, _backColorPointsPanel, e);
        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel10, _backColorPointsPanel, e);
        }
    }
}