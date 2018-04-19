﻿using System;
using System.Collections.Generic;
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

        public ControlPanelController()
        {
            _teams = new Team[NumberOfTemas];
            for (var i = 0; i < _teams.Length; i++)
            {
                _teams[i] = new Team();
            }

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

                _categorys.RemoveAll(i => !i.Questions.Any());
            }

            if (_categorys.Count <= 0)
            {
                MessageBox.Show(@"Não exitem preguntas", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
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
            if (_nextQuest != null)
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
    }
}