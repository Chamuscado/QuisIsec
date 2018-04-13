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
            _view.Close();
            _parent?.GameViewControllerWasEnd();
        }
    }
}