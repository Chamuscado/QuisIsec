using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using lib;

namespace QuisIsec
{
    public class ControlPanelController
    {
        private IControlPanelView _view;
        private GameViewController _gamController;

        public ControlPanelController()
        {
            _view = new ControlPanel();
            _view.SetController(this);
            LoadFiles();
            _view.Show();
            _gamController = new GameViewController();
            _gamController.SetQuest(_categorys[3].Questions[1]);
        }

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
            }
        }
    }
}