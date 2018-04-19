using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using lib;

namespace QuisIsec
{
    public class ControlPanelController
    {
        private const int NumberOfTemas = 2;
        private IControlPanelView _view;
        private GameViewController _gameController;
        private Question _nextQuest;
        private Team[] _teams;
        private bool autoShow = true;
        private const string UsedNameFile = @"UsedQuestions.txt";
        private string _filesPath;
        private StreamWriter _writerUseds;

        public ControlPanelController()
        {
            _teams = new Team[NumberOfTemas];
            for (var i = 0; i < _teams.Length; i++)
            {
                _teams[i] = new Team();
            }

            _view = new ControlPanel();
            _view.SetController(this);

            _view.Show();
            _view.RefreshDataGridView(_categorys);
        }

        private string[] _toIgnore = {"<categoria>"};
        private string[] _toIgnoreFiles = {UsedNameFile};
        private List<Category> _categorys = new List<Category>();
        private List<Question> _used = new List<Question>();

        public void LoadFiles()
        {
            if (_view.RequestNameFiles(out var nameFiles))
            {
                if (nameFiles.Length > 0)
                    _filesPath = Path.GetDirectoryName(nameFiles[0]);
                foreach (var nameFile in nameFiles)
                {
                    if (_toIgnoreFiles.Any(toIgnore =>
                        string.Compare(nameFile, _filesPath + "\\" + toIgnore,
                            StringComparison.InvariantCultureIgnoreCase) == 0))
                        continue;
                    var file = new CsvFile(nameFile);
                    if (!file.FileExists() || !file.Open())
                        continue;

                    while (file.HasNextLine())
                    {
                        var line = file.GetNextLine();
                        if (line.Count < 6)
                            continue;
                        var cat = line[0];
                        if (_toIgnore.Any(toIgnore =>
                            string.Compare(cat, toIgnore, StringComparison.InvariantCultureIgnoreCase) == 0))
                            continue;
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

                        if (line.Count >= 3 && line.All(item => item.Any()))
                            _categorys[i].AddQuestion(new Question(cat, quest, line));
                    }

                    _categorys.Sort();
                    file.Close();
                }


                var path = _filesPath + "\\" + UsedNameFile;
                if (File.Exists(path))
                    using (var file = new CsvFile(path, open: true))
                    {
                        while (file.HasNextLine())
                        {
                            var line = file.GetNextLine();
                            if (line.Count < 6)
                                continue;
                            var cat = line[0];
                            line.RemoveAt(0);
                            var quest = line[0];
                            line.RemoveAt(0);
                            if (line.Count >= 3 && line.All(item => item.Any()))
                                _used.Add(new Question(cat, quest, line));
                        }
                    }

                foreach (var question in _used)
                {
                    var quest = question;
                    foreach (var category in _categorys)
                    {
                        var catName = category.Name;
                        if (quest.Category.CompareTo(catName) == 0)
                        {
                            for (var i = 0; i < category.Questions.Count; ++i)
                            {
                                var array = category.Questions;
                                if (array[i].Equals(quest))
                                {
                                    category.Questions.RemoveAt(i);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                }
            }

            _categorys.RemoveAll(i => !i.Questions.Any());

            if (_categorys.Count <= 0)
            {
                MessageBox.Show(@"Não exitem preguntas", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            if (_writerUseds == null)
                _writerUseds = new StreamWriter(_filesPath + "\\" + UsedNameFile, true) {AutoFlush = true};
            _view.RefreshDataGridView(categorys: _categorys);
        }

        public void NewQuest()
        {
            if (_categorys.Count <= 0)
            {
                MessageBox.Show(@"Não exitem mais preguntas", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_categorys.Count > 0)
            {
                var rand1 = new Random().Next(_categorys.Count);
                _nextQuest = _categorys[rand1].Questions[new Random().Next(_categorys[rand1].Questions.Count)];
                _view.Quest = _nextQuest.Quest;
                _view.Category = _nextQuest.Category;
                _view.RightAnswer = _nextQuest.RightAnswer;
                _view.Answer1 = _nextQuest.OthersAnswer[0];
                _view.Answer2 = _nextQuest.OthersAnswer[1];
                _view.Answer3 = _nextQuest.OthersAnswer[2];
            }
        }

        public void QuestToGameWindow()
        {
            if (_gameController == null)
            {
                MessageBox.Show(@"Primeiro inicie a janela de jogo!!");
                return;
            }

            if (_nextQuest != null)
                _gameController.SetQuest(_nextQuest);
            _used.Add(_nextQuest);
            _writerUseds.WriteLine(
                $"{_nextQuest?.Category};{_nextQuest?.Quest};" +
                $"{_nextQuest?.RightAnswer};{_nextQuest?.OthersAnswer[0]};" +
                $"{_nextQuest?.OthersAnswer[1]};{_nextQuest?.OthersAnswer[2]}");
            foreach (var category in _categorys)
            {
                if (string.Compare(category.Name, _nextQuest?.Category, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    for (var i = 0; i < category.Questions.Count; ++i)
                    {
                        if (category.Questions[i].Equals(_nextQuest))
                        {
                            category.Questions.RemoveAt(i);
                            _categorys.RemoveAll(cat => !cat.Questions.Any());
                            _view.RefreshDataGridView(_categorys);
                            return;
                        }
                    }
                }
            }
        }


        public bool CloseResquest()
        {
            _gameController.End();

            Application.Exit();
            return false;
        }

        public void GameViewControllerWasEnd()
        {
            _gameController = null;
        }

        public void TeamNameChanged(int i, string teamName1)
        {
            if (i >= 0 && i < _teams.Length)
            {
                _teams[i].Name = teamName1;
                if (autoShow)
                    _gameController.ChangedTeamInformation(_teams);
            }
        }

        public void TeamPointsChanged(int i, int points)
        {
            if (i >= 0 && i < _teams.Length)
            {
                _teams[i].Points = points;
                if (autoShow)
                    _gameController.ChangedTeamInformation(_teams);
            }
        }

        public void RefreshGameWindow()
        {
            _gameController.ChangedTeamInformation(_teams);
        }

        public void TeamColorChanged(int i, Color color)
        {
            if (i >= 0 && i < _teams.Length)
            {
                _teams[i].Color = color;
                if (autoShow)
                    _gameController.ChangedTeamInformation(_teams);
            }
        }

        public void StartGameWin()
        {
            if (_gameController == null) _gameController = new GameViewController();
            else _gameController.BringToFront();
        }
    }
}