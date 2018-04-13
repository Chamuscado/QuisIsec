using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using lib;

namespace QuisIsec
{
    public class ControlPanelController
    {
        private IControlPanelView _view;
        private GameViewController _gameController;
        private Question _nextQuest;

        public ControlPanelController()
        {
            _view = new ControlPanel();
            _view.SetController(this);
            LoadFiles();
            _view.Show();
            _gameController = new GameViewController();
            _view.RefreshDataGridView(_categorys);
            NewQuest();
        }

        private string[] _toIgnore = {"<categoria>"};
        private List<Category> _categorys = new List<Category>();

        private void LoadFiles()
        {
            if (_view.RequestNameFiles(out var nameFiles))
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

                        if (line.All(item => item.Any()))
                            _categorys[i].AddQuestion(new Question(quest, line));
                    }

                    _categorys.Sort();
                    file.Close();
                }

                _categorys.RemoveAll(i => !i.Questions.Any());

                var cont = 0;
                foreach (var category in _categorys)
                {
                    cont += category.Questions.Count;
                }
            }
        }

        public void NewQuest()
        {
            var rand1 = new Random().Next(_categorys.Count);
            _nextQuest = _categorys[rand1].Questions[new Random().Next(_categorys[rand1].Questions.Count)];
            _view.Quest = _nextQuest.Quest;
            _view.RightAnswer = _nextQuest.RightAnswer;
            _view.Answer1 = _nextQuest.OthersAnswer[0];
            _view.Answer2 = _nextQuest.OthersAnswer[1];
            _view.Answer3 = _nextQuest.OthersAnswer[2];
        }

        public void QuestToGameWindow()
        {
            _gameController.SetQuest(_nextQuest);
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
    }
}