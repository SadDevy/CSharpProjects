using System;
using System.Linq;
using System.Windows.Forms;
using NLog;
using NLog.Config;

namespace AsyncAndAwaitUI
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        private static Logger logger = LogManager.GetCurrentClassLogger();

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
