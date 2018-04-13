using System.Windows.Forms;

namespace QuisIsec
{
    public interface IView
    {
        void Show();
        void Close();

        bool Enabled { get; set; }
        Form Form { get; }
    }
}