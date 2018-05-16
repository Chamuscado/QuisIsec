using System;
using System.Drawing;
using lib;
using lib.Interfaces;

namespace QuIsec_Client
{
    public class GameViewController : IGameViewController
    {
        private IGameView _view;
        private FileTransferClient _parent;

        public GameViewController(string ip)
        {
            _parent = new FileTransferClient(this);
            _parent.ip = ip;
            _parent.Start();
            _view = new QuIsec();
            _view.SetController(this);
            _view.Show();
        }

        public void SetQuest(Question quest)
        {
            _view.SetQuestThreadSafe(quest);
        }

        public bool End(bool fromView = false)
        {
            if (!fromView)
                _view?.Close();
            _parent.Stop();
            Environment.Exit(0);
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
            _view.ChangedTeamInformationThreadSafe(teams);
         }


    public void BringToFront()
        {
            _view.BringToFront();
        }

        public void SetTime(int remainTime)
        {
            _view.SetTimeThreadSafe(remainTime);
        }

        public void ShowRightAnswer(string currentQuestRightAnswer)
        {
            _view.ShowRightAnswerThreadSafe(currentQuestRightAnswer);
        }
    }
}