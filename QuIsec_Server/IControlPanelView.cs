using System.Collections.Generic;
using lib;
using lib.Interfaces;

namespace QuIsec_Server
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
        string Category { get; set; }
        string TimeBox { get; set; }
        string Team0Points { get; set; }
        string Team1Points { get; set; }
        void RefreshDataGridView(ICollection<Category> categorys);
    }
}