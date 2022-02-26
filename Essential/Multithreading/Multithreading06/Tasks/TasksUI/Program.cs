using NLog;
using NLog.Config;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TasksUI
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string configName = args.First();
            LogManager.Configuration = new XmlLoggingConfiguration(configName);

            logger.Info("Запуск приложения.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormUI());

            logger.Info("Завершение приложения.");
        }
    }
}
