using System;
using System.Windows.Forms;

namespace QuIsec_Client
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            args = new[] {"127.0.0.1"};
#else
            if (args.Length != 1)
            {
                Console.Out.WriteLine("QuIsecCliet <ip_server>");
                return;
            }
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new GameViewController(args[0]);
            Application.Run();
        }

    }
}