namespace QuisIsec
{
    public interface IControlPanelView:IView
    {
        void SetController(ControlPanelController controller);
        bool RequestNameFiles(out string[] nameFile);
    }
}