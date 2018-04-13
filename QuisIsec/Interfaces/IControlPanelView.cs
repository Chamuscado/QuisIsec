using System.Collections.Generic;

namespace QuisIsec
{
    public interface IControlPanelView : IView
    {
        void SetController(ControlPanelController controller);
        bool RequestNameFiles(out string[] nameFile);
        string Quest { get; set; }
        string RightAnswer { get; set; }
        string Answer1 { get; set; }
        string Answer2 { get; set; }
        string Answer3 { get; set; }
        void RefreshDataGridView(ICollection<Category> categorys);
    }
}