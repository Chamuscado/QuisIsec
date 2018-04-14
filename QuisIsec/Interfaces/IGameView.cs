namespace QuisIsec
{
    public interface IGameView : IView
    {
        void SetController(GameViewController controller);
        string Quest { get; set; }
        string Answer0 { get; set; }
        string Answer1 { get; set; }
        string Answer2 { get; set; }
        string Answer3 { get; set; }
        string Team0Name { get; set; }
        string Team1Name { get; set; }
        int Team0Points { get; set; }
        int Team1Points { get; set; }
    }
}