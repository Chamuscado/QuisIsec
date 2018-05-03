//#define TablePanelBlue

#define TextAutoSize
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace QuisIsec
{
    public partial class QuIsec : MetroFramework.Forms.MetroForm, IGameView
    {
        private GameViewController _controller;
        public int LabelsAnswerCornerRadius { get; set; }
        public int LabelsCategoryCornerRadius { get; set; }
        public int TextHeightMargin { get; set; } = 10;
        public int TextWidthMargin { get; set; } = 10;

        public float RadiosFactor { get; set; } = 1.2f;


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
            set => labelAnswer0Team0.Text =
                labelAnswer1Team0.Text =
                    labelAnswer2Team0.Text =
                        labelAnswer3Team0.Text =
                            team0Name.Text = value;
        }

        public string Team1Name
        {
            get => team1Name.Text;
            set => labelAnswer0Team1.Text =
                labelAnswer1Team1.Text =
                    labelAnswer2Team1.Text =
                        labelAnswer3Team1.Text =
                            team1Name.Text = value;
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

        public Color Team0Color
        {
            get => _team0Color;
            set
            {
                tableLayoutPanel9.Invalidate();
                _team0Color = value;
            }
        }

        public Color Team1Color
        {
            get => _team1Color;
            set
            {
                tableLayoutPanel10.Invalidate();
                _team1Color = value;
            }
        }

        public string Category
        {
            get => categoryLabel.Text;
            set => categoryLabel.Text = value;
        }

        public Answer Team0Answer
        {
            get
            {
                var answer = Answer.None;
                if (labelAnswer0Team0.Visible)
                    answer = Answer.A;
                else if (labelAnswer1Team0.Visible)
                    answer = Answer.B;
                else if (labelAnswer2Team0.Visible)
                    answer = Answer.C;
                else if (labelAnswer3Team0.Visible)
                    answer = Answer.D;
                return answer;
            }
            set
            {
                switch (value)
                {
                    case Answer.None:
                        labelAnswer0Team0.Visible =
                            labelAnswer1Team0.Visible =
                                labelAnswer2Team0.Visible =
                                    labelAnswer3Team0.Visible = false;
                        break;
                    case Answer.A:
                        labelAnswer0Team0.Visible = true;
                        labelAnswer1Team0.Visible =
                            labelAnswer2Team0.Visible =
                                labelAnswer3Team0.Visible = false;
                        break;
                    case Answer.B:
                        labelAnswer0Team0.Visible =
                            labelAnswer2Team0.Visible =
                                labelAnswer3Team0.Visible = false;
                        labelAnswer1Team0.Visible = true;

                        break;
                    case Answer.C:
                        labelAnswer0Team0.Visible =
                            labelAnswer1Team0.Visible =
                                labelAnswer3Team0.Visible = false;
                        labelAnswer2Team0.Visible = true;
                        break;
                    case Answer.D:
                        labelAnswer0Team0.Visible =
                            labelAnswer1Team0.Visible =
                                labelAnswer2Team0.Visible = false;
                        labelAnswer3Team0.Visible = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        public Answer Team1Answer
        {
            get
            {
                var answer = Answer.None;
                if (labelAnswer0Team1.Visible)
                    answer = Answer.A;
                else if (labelAnswer1Team1.Visible)
                    answer = Answer.B;
                else if (labelAnswer2Team1.Visible)
                    answer = Answer.C;
                else if (labelAnswer3Team1.Visible)
                    answer = Answer.D;
                return answer;
            }
            set
            {
                switch (value)
                {
                    case Answer.None:
                        labelAnswer0Team1.Visible =
                            labelAnswer1Team1.Visible =
                                labelAnswer2Team1.Visible =
                                    labelAnswer3Team1.Visible = false;
                        break;
                    case Answer.A:
                        labelAnswer0Team1.Visible = true;
                        labelAnswer1Team1.Visible =
                            labelAnswer2Team1.Visible =
                                labelAnswer3Team1.Visible = false;
                        break;
                    case Answer.B:
                        labelAnswer0Team1.Visible =
                            labelAnswer2Team1.Visible =
                                labelAnswer3Team1.Visible = false;
                        labelAnswer1Team1.Visible = true;

                        break;
                    case Answer.C:
                        labelAnswer0Team1.Visible =
                            labelAnswer1Team1.Visible =
                                labelAnswer3Team1.Visible = false;
                        labelAnswer2Team1.Visible = true;
                        break;
                    case Answer.D:
                        labelAnswer0Team1.Visible =
                            labelAnswer1Team1.Visible =
                                labelAnswer2Team1.Visible = false;
                        labelAnswer3Team1.Visible = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        public int Time
        {
            set
            {
                if (value < 1000) //less than second
                    timerLabel.Text = $"00:{(value / 10):00}";
                else if (value < 60000) // less than minute
                    if (value % 1000 == 0)
                        timerLabel.Text = $"{(value / 1000):00}";
                    else
                        timerLabel.Text = $"{(value / 1000):00}:{(value % 1000 / 10):00}";
                else // mode than minute
                    timerLabel.Text = $"{value / 60000}:{(value % 60000 / 1000):00}";
            }
        }

        public Form Form => this;

        public QuIsec()
        {
            InitializeComponent();
            Team0Answer = Answer.A;
            Team1Answer = Answer.B;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            MaximizeToSecondaryMonitor();
        }

        public void MaximizeToSecondaryMonitor()
        {
            var secondaryScreen = Screen.AllScreens.FirstOrDefault(s => !s.Primary);

            if (secondaryScreen != null)
            {
                var workingArea = secondaryScreen.WorkingArea;
                Left = workingArea.Left;
                Top = workingArea.Top;
                Width = workingArea.Width;
                Height = workingArea.Height;
            }
        }

        private readonly Color _backColorQuest = Color.FromArgb(5, 100, 187);
        private readonly Color _backColorBackPanel = Color.FromArgb(0, 134, 191);
        private readonly Color _backColorAnswer = Color.FromArgb(5, 100, 187);
        private readonly Color _backColorPointsPanel = Color.FromArgb(0, 134, 191);
        private readonly Color _backColorCategoryLabel = Color.Blue;
        private readonly Color _backColorTimer = Color.Cornsilk;
        private Color _team0Color;
        private Color _team1Color;


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel1, _backColorBackPanel, e, resizeText: false);
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel3, _backColorAnswer, e, resizeText: false);
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel4, _backColorAnswer, e, resizeText: false);
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel5, _backColorAnswer, e, resizeText: false);
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel6, _backColorAnswer, e, resizeText: false);
        }

        private void PaintRoundEdges(Control graphElement, Color color, PaintEventArgs e, int cornerRadius = 40,
            bool resizeText = true)
        {
            if (cornerRadius > 0)

                using (var graphicsPath = _getRoundRectangle(graphElement.ClientRectangle, cornerRadius))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var brush = new SolidBrush(color))
                        e.Graphics.FillPath(brush, graphicsPath);
                    using (var pen = new Pen(color, 1.0f))
                        e.Graphics.DrawPath(pen, graphicsPath);
                    TextRenderer.DrawText(e.Graphics, graphElement.Text, graphElement.Font,
                        graphElement.ClientRectangle,
                        graphElement.ForeColor);
                }

            if (resizeText && graphElement.Text.Any())
                Lables_TextAutoSize(graphElement, e);
        }

        private GraphicsPath _getRoundRectangle(Rectangle rectangle, int cornerRadius)
        {
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
            PaintRoundEdges(tableLayoutPanel8, _backColorBackPanel, e, resizeText: false);
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {
#if TablePanelBlue
            PaintRoundEdges(tableLayoutPanel9, _backColorPointsPanel, e);
#else
            PaintRoundEdges(tableLayoutPanel9, Team0Color, e, resizeText: false);
#endif
        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {
#if TablePanelBlue
            PaintRoundEdges(tableLayoutPanel10, _backColorPointsPanel, e, resizeText: false);
#else
            PaintRoundEdges(tableLayoutPanel10, Team1Color, e, resizeText: false);
#endif
        }

        private void categoryLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(categoryLabel, _backColorCategoryLabel, e, LabelsCategoryCornerRadius);
        }

        private void questpanel_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(questPanel, _backColorQuest, e, resizeText: false);
        }

        private void QuisIsec_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _controller.End(true);
        }

        private void timerLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(timerLabel, _backColorTimer, e);
        }

        private void labelAnswer0Team0_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer0Team0, _team0Color, e, LabelsAnswerCornerRadius);
        }


        private void labelAnswer1Team0_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer1Team0, _team0Color, e, LabelsAnswerCornerRadius);
        }

        private void labelAnswer2Team0_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer2Team0, _team0Color, e, LabelsAnswerCornerRadius);
        }

        private void labelAnswer3Team0_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer3Team0, _team0Color, e, LabelsAnswerCornerRadius);
        }

        private void labelAnswer0Team1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer0Team1, _team1Color, e, LabelsAnswerCornerRadius);
        }

        private void labelAnswer1Team1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer1Team1, _team1Color, e, LabelsAnswerCornerRadius);
        }

        private void labelAnswer2Team1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer2Team1, _team1Color, e, LabelsAnswerCornerRadius);
        }

        private void labelAnswer3Team1_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(labelAnswer3Team1, _team1Color, e, LabelsAnswerCornerRadius);
        }

        private void QuIsec_Resize(object sender, EventArgs e)
        {
            LabelsCategoryCornerRadius = (int) (categoryLabel.Height / RadiosFactor);
            LabelsAnswerCornerRadius = (int) (labelAnswer0Team0.Height / RadiosFactor);
        }

        private float stepFontSize = 0.5f;
        private int ToloranceToResizeText = 5;

        private void Lables_TextAutoSize(Control control, PaintEventArgs e)
        {
#if TextAutoSize
            var textRender = TextRenderer.MeasureText(control.Text,
                new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style));
            var prefectWidth = control.Width - TextWidthMargin * 2;
            var prefectHeight = control.Height - TextHeightMargin * 2;

            if (Math.Abs(prefectWidth - textRender.Width) > ToloranceToResizeText ||
                Math.Abs(prefectHeight - textRender.Height) > ToloranceToResizeText)
                control.Font = new Font(control.Font.FontFamily, stepFontSize,
                    control.Font.Style);
            while (true)
            {
                textRender = TextRenderer.MeasureText(control.Text,
                    new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style));

                if (prefectWidth < textRender.Width || prefectHeight < textRender.Height)
                    break;

                control.Font = new Font(control.Font.FontFamily, control.Font.Size + stepFontSize,
                    control.Font.Style);
            }
#endif
        }


        private void preguntaLabel_Paint(object sender, PaintEventArgs e)
        {
            Lables_TextAutoSize(preguntaLabel, e);
        }

        private void resposta_0_Paint(object sender, PaintEventArgs e)
        {
            Lables_TextAutoSize(resposta_0, e);
        }

        private void resposta_1_Paint(object sender, PaintEventArgs e)
        {
            Lables_TextAutoSize(resposta_1, e);
        }

        private void resposta_2_Paint(object sender, PaintEventArgs e)
        {
            Lables_TextAutoSize(resposta_2, e);
        }

        private void resposta_3_Paint(object sender, PaintEventArgs e)
        {
            Lables_TextAutoSize(resposta_3, e);
        }
    }
}