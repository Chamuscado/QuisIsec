using System.Drawing;

namespace QuisIsec
{
    public class GameViewController
    {
        private IGameView _view;
        private ControlPanelController _parent;

        public GameViewController()
        {
            _view = new QuisIsec();
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
            var list = quest.Answers.Clone();
            list.Shuffle();
            _view.Answer0 = list[0];
            _view.Answer1 = list[1];
            _view.Answer2 = list[2];
            _view.Answer3 = list[3];
        }

        public void End()
        {
            _view?.Close();
            _parent?.GameViewControllerWasEnd();
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

        public Color Team0Color {
            get => _view.Team0Color;
            set => _view.Team0Color = value;
        }

        public Color Team1Color {
            get => _view.Team1Color;
            set => _view.Team1Color = value;
        }

        public void ChangedTeamInformation(Team[] teams)
        {
            Team0Name = teams[0].Name;
            Team1Name = teams[1].Name;
            Team0Points = teams[0].Points;
            Team1Points = teams[1].Points;
            Team0Color = teams[0].Color;
            Team1Color = teams[1].Color;
        }

        public void BringToFront()
        {
            _view.BringToFront();
        }
    }
}