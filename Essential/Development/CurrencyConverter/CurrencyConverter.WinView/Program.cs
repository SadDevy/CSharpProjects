using CurrencyConverter.Models;
using System;
using System.Windows.Forms;

namespace CurrencyConverter.WinView
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormView view = new FormView();
            Presenter presenter = new Presenter(view);
            Application.Run(view);
        }
    }
}
