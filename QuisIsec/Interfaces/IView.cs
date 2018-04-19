using System.Windows.Forms;

namespace QuisIsec
{
    public interface IView
    {
        void Show();
        void Close();
        void BringToFront();
    }
}