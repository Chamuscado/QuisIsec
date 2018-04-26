using System;
using System.Drawing;

namespace QuisIsec
{
    public class GameViewController
    {
        private IGameView _view;
        private ControlPanelController _parent;

        public GameViewController()
        {
            _view = new QuIsec();
            _view.SetController(this);
            _view.Show();
        }

        public void SetParent(ControlPanelController parent)
        {
            _parent = parent;
        }

        public void SetQuest(Question quest)
        {
            _view.Quest = quest.Quest;
            _view.Category = quest.Category;
            var list = quest.Answers.Clone();
            list.Shuffle();
            _view.Answer0 = list[0];
            _view.Answer1 = list[1];
            _view.Answer2 = list[2];
            _view.Answer3 = list[3];
        }

        public bool End(bool fromView = false)
        {
            if (!fromView)
                _view?.Close();
            _parent?.GameViewControllerWasEnd();
            return false;
        }

        public string Team0Name
        {
            get => _view.Team0Name;
            set => _view.Team0Name = value;
        }

        public string Team1Name
        {
            get => _view.Team1Name;
            set => _view.Team1Name = value;
        }

        public int Team0Points
        {
            get => _view.Team0Points;
            set => _view.Team0Points = value;
        }

        public int Team1Points
        {
            get => _view.Team1Points;
            set => _view.Team1Points = value;
        }

        public Color Team0Color
        {
            get => _view.Team0Color;
            set => _view.Team0Color = value;
        }

        public Color Team1Color
        {
            get => _view.Team1Color;
            set => _view.Team1Color = value;
        }

        public Answer Team0Answer
        {
            get => _view.Team0Answer;
            set => _view.Team0Answer = value;
        }

        public Answer Team1Answer
        {
            get => _view.Team1Answer;
            set => _view.Team1Answer = value;
        }

        public void ChangedTeamInformation(Team[] teams)
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


        public void BringToFront()
        {
            _view.BringToFront();
        }

        public void SetTime(int remainTime)
        {
            _view.Time = remainTime;
        }
    }
}