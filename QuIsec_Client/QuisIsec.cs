//#define TablePanelBlue

#define TextAutoSize

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using lib;
using Timer = System.Threading.Timer;

namespace QuIsec_Client
{
    public partial class QuIsec : MetroFramework.Forms.MetroForm, IGameView
    {
        private GameViewController _controller;
        public int LabelsAnswerCornerRadius { get; set; }
        public int LabelsCategoryCornerRadius { get; set; }
        public int TextHeightMargin { get; set; } = 10;
        public int TextWidthMargin { get; set; } = 5;

        public float RadiosFactor { get; set; } = 1.2f;

        private Color[] _answerBackColors;


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
            set
            {
                resposta_0.Text = value;
                _answerBackColors[0] = _backColorAnswer;
                tableLayoutPanel_AnswerA.Invalidate();
            }
        }

        public string Answer1
        {
            get => resposta_1.Text;
            set
            {
                resposta_1.Text = value;
                _answerBackColors[1] = _backColorAnswer;
                tableLayoutPanel_AnswerB.Invalidate();
            }
        }

        public string Answer2
        {
            get => resposta_2.Text;
            set
            {
                resposta_2.Text = value;
                _answerBackColors[2] = _backColorAnswer;
                tableLayoutPanel_AnswerC.Invalidate();
            }
        }

        public string Answer3
        {
            get => resposta_3.Text;
            set
            {
                resposta_3.Text = value;
                _answerBackColors[3] = _backColorAnswer;
                tableLayoutPanel_AnswerD.Invalidate();
            }
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
                tableLayoutPanel_PointsTeam0.Invalidate();
                _team0Color = value;
            }
        }

        public Color Team1Color
        {
            get => _team1Color;
            set
            {
                tableLayoutPanel_PointsTeam1.Invalidate();
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
                
                //timerLabel.Text = value.ToString();
            }
        }


        private delegate void ShowRightAnswerThreadSafeDelegate(string currentQuestRightAnswer);

        public void ShowRightAnswerThreadSafe(string currentQuestRightAnswer)
        {
            if (InvokeRequired)
                Invoke(new ShowRightAnswerThreadSafeDelegate(ShowRightAnswerThreadSafe), currentQuestRightAnswer);
            else
            {
                //RandomizeRight(10000, 1000, 1000);
                //Thread.Sleep(11000);
                if (string.Compare(Answer0, currentQuestRightAnswer, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (_answerBackColors.Length >= 4)
                        _answerBackColors[0] = _rightAnswerColor;
                    tableLayoutPanel_AnswerA.Invalidate();
                }
                else if (string.Compare(Answer1, currentQuestRightAnswer, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (_answerBackColors.Length >= 4)
                        _answerBackColors[1] = _rightAnswerColor;
                    tableLayoutPanel_AnswerB.Invalidate();
                }
                else if (string.Compare(Answer2, currentQuestRightAnswer, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (_answerBackColors.Length >= 4)
                        _answerBackColors[2] = _rightAnswerColor;
                    tableLayoutPanel_AnswerC.Invalidate();
                }
                else if (string.Compare(Answer3, currentQuestRightAnswer, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (_answerBackColors.Length >= 4)
                        _answerBackColors[3] = _rightAnswerColor;
                    tableLayoutPanel_AnswerD.Invalidate();
                }
            }
        }

        
        private delegate void ChangedTeamInformationDelegate(Team[] teams);

        public void ChangedTeamInformationThreadSafe(Team[] teams)
        {
            if (InvokeRequired)
                Invoke(new ChangedTeamInformationDelegate(ChangedTeamInformationThreadSafe), new object[]{ teams });
            else
            {
                if (Team0Name.CompareTo(teams[0].Name) != 0)
                    Team0Name = teams[0].Name;
                if (Team1Name.CompareTo(teams[1].Name) != 0)
                    Team1Name = teams[1].Name;
                if (Team0Points != teams[0].Points)
                    Team0Points = teams[0].Points;
                if (Team1Points != teams[1].Points)
                    Team1Points = teams[1].Points;
                if (!Team0Color.Equals(teams[0].Color))
                    Team0Color = teams[0].Color;
                if (!Team1Color.Equals(teams[1].Color))
                    Team1Color = teams[1].Color;
                if (Team0Answer != teams[0].Answer)
                    Team0Answer = teams[0].Answer;
                if (Team1Answer != teams[1].Answer)
                    Team1Answer = teams[1].Answer;
            }
        }



        private delegate void SetTimeThreadSafeDelegate(int remainTime);

        public void SetTimeThreadSafe(int remainTime)
        {
            if (InvokeRequired)
                Invoke(new SetTimeThreadSafeDelegate(SetTimeThreadSafe), remainTime);
            else
            {
                Time = remainTime;
            }
        }

        private delegate void SetQuestThreadSafeDelegate(Question quest);

        public void SetQuestThreadSafe(Question quest)
        {
            if (InvokeRequired)
                Invoke(new SetQuestThreadSafeDelegate(SetQuestThreadSafe), quest);
            else
            {
                Quest = quest.Quest;
                Category = quest.Category;
                var list = quest.Answers.Clone();
                list.Shuffle();
                Answer0 = list[0];
                Answer1 = list[1];
                Answer2 = list[2];
                Answer3 = list[3];
            }
        }


        #region RandomizeRight

        private Timer _bigTimer;
        private Timer _smallTimerSelect;
        private Timer _smallTimerUnselect;

        private void RandomizeRight(int bigTime, int smallTimeSelect, int smallTimeUnselect)
        {
            if (bigTime <= 0 || _bigTimer != null)
                return;
            _bigTimer = new Timer(EndRandomize, null, 0, bigTime);
            if (smallTimeSelect > 0)
                _smallTimerSelect = new Timer(RandomRight, null, 0, smallTimeSelect + smallTimeUnselect);
            if (smallTimeUnselect > 0)
                _smallTimerUnselect = new Timer(Callback, null, smallTimeSelect, smallTimeUnselect + smallTimeSelect);
        }

        private void Callback(object state)
        {
            if (_answerBackColors.Length == 0)
                return;
            var rand = new Random().Next(_answerBackColors.Length);
            for (var i = 0; i < _answerBackColors.Length; ++i)
            {
                _answerBackColors[i] = _backColorAnswer;
            }

            InvalidateAnswers();
        }

        private void RandomRight(object state)
        {
            if (_answerBackColors.Length == 0)
                return;
            var rand = new Random().Next(_answerBackColors.Length);
            for (var i = 0; i < _answerBackColors.Length; ++i)
            {
                _answerBackColors[i] = rand == i ? _rightAnswerColor : _backColorAnswer;
            }

            InvalidateAnswers();
        }

        private void InvalidateAnswers()
        {
            tableLayoutPanel_AnswerA.Invalidate();
            tableLayoutPanel_AnswerB.Invalidate();
            tableLayoutPanel_AnswerC.Invalidate();
            tableLayoutPanel_AnswerD.Invalidate();
        }

        private void EndRandomize(object state)
        {
            _bigTimer?.Dispose();
            _smallTimerSelect?.Dispose();
            _smallTimerUnselect?.Dispose();
            _bigTimer = null;
            _smallTimerSelect = null;
            _smallTimerUnselect = null;
        }

        #endregion

        public Form Form => this;

        public QuIsec()
        {
            InitializeComponent();
            Team0Answer = Answer.None;
            Team1Answer = Answer.None;
            _answerBackColors = new Color[4];
            for (var i = 0; i < _answerBackColors.Length; ++i)
            {
                _answerBackColors[i] = _backColorAnswer;
            }
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
        private Color _rightAnswerColor = Color.Green;
        private Color _team0Color;
        private Color _team1Color;


        private void tableLayoutPanel_GameArea_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel_GameArea, _backColorBackPanel, e, resizeText: false);
        }

        private void tableLayoutPanel_AnswerA_Paint(object sender, PaintEventArgs e)
        {
            if (_answerBackColors.Length >= 4)
                PaintRoundEdges(tableLayoutPanel_AnswerA, _answerBackColors[0], e, resizeText: false);
        }

        private void tableLayoutPanel_AnswerB_Paint(object sender, PaintEventArgs e)
        {
            if (_answerBackColors.Length >= 4)
                PaintRoundEdges(tableLayoutPanel_AnswerB, _answerBackColors[1], e, resizeText: false);
        }

        private void tableLayoutPanel_AnswerC_Paint(object sender, PaintEventArgs e)
        {
            if (_answerBackColors.Length >= 4)
                PaintRoundEdges(tableLayoutPanel_AnswerC, _answerBackColors[2], e, resizeText: false);
        }

        private void tableLayoutPanel_AnswerD_Paint(object sender, PaintEventArgs e)
        {
            if (_answerBackColors.Length >= 4)
                PaintRoundEdges(tableLayoutPanel_AnswerD, _answerBackColors[3], e, resizeText: false);
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
                Lables_TextAutoSize(graphElement);
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

        private void tableLayoutPanel_PointsAndTime_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(tableLayoutPanel_PointsAndTime, _backColorBackPanel, e, resizeText: false);
        }

        private void tableLayoutPanel_PointsTeam0_Paint(object sender, PaintEventArgs e)
        {
#if TablePanelBlue
            PaintRoundEdges(tableLayoutPanel_PointsTeam0, _backColorPointsPanel, e);
#else
            PaintRoundEdges(tableLayoutPanel_PointsTeam0, Team0Color, e, resizeText: false);
#endif
        }

        private void tableLayoutPanel_PointsTeam1_Paint(object sender, PaintEventArgs e)
        {
#if TablePanelBlue
            PaintRoundEdges(tableLayoutPanel_PointsTeam1, _backColorPointsPanel, e, resizeText: false);
#else
            PaintRoundEdges(tableLayoutPanel_PointsTeam1, Team1Color, e, resizeText: false);
#endif
        }

        private void categoryLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintRoundEdges(categoryLabel, _backColorCategoryLabel, e, LabelsCategoryCornerRadius);
        }

        private void questpanel_Paint(object sender, PaintEventArgs e)
        {
            //PaintRoundEdges(questPanel, _backColorQuest, e, resizeText: false);
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


        private void Lables_TextAutoSize(Control control)
        {
#if TextAutoSize

            //var textRender = TextRenderer.MeasureText(control.Text, new Font(control.Font.FontFamily, 2, control.Font.Style));

            var prefectWidth = control.Width - TextWidthMargin * 2;
            var prefectHeight = control.Height - TextHeightMargin * 2;

            //if (Math.Abs(prefectWidth  - textRender.Width ) > ToloranceToResizeText ||
            //    Math.Abs(prefectHeight - textRender.Height) > ToloranceToResizeText)
            //    control.Font = new Font(control.Font.FontFamily, stepFontSize,
            //        control.Font.Style);
            //while (true)
            //{
            //    textRender = TextRenderer.MeasureText(control.Text,
            //        new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style));
            //
            //    if (prefectWidth < textRender.Width || prefectHeight < textRender.Height)
            //        break;
            //
            //    control.Font = new Font(control.Font.FontFamily, control.Font.Size + stepFontSize,
            //        control.Font.Style);
            //}

            for (var fontSize = 2; fontSize <= 72; fontSize++)
            {
                var font = new Font(control.Font.FontFamily, fontSize, control.Font.Style);
                var textSize = TextRenderer.MeasureText(control.Text, font);

                if (textSize.Width > prefectWidth || textSize.Height > prefectHeight)
                {
                    fontSize = fontSize - 5;
                    if (fontSize < 0) fontSize = 1;
                    control.Font = new Font(control.Font.FontFamily, fontSize, control.Font.Style);
                    break;
                }
            }
        }

#endif


        /*
        private void Lables_TextAutoSize(Control control)
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
        */


        private void preguntaLabel_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(preguntaLabel);
        }

        private void resposta_0_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_0);
        }

        private void resposta_1_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_1);
        }

        private void resposta_2_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_2);
        }

        private void resposta_3_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_3);
        }

        private void resposta_0_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_0);
        }

        private void resposta_1_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_1);
        }

        private void resposta_2_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_2);
        }

        private void resposta_3_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(resposta_3);
        }

        private void categoryLabel_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(categoryLabel);
        }

        private void A_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(A);
        }

        private void B_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(B);
        }

        private void C_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(C);
        }

        private void D_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(D);
        }

        private void team0Name_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(team0Name);
        }

        private void team1Name_Resize(object sender, EventArgs e)
        {
            Lables_TextAutoSize(team1Name);
        }

        private void team0Name_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(team0Name);
        }

        private void team1Name_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(team1Name);
        }

        private void timerLabel_TextChanged(object sender, EventArgs e)
        {
            Lables_TextAutoSize(timerLabel);
        }
    }
}