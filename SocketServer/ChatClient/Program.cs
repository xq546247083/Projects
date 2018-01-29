using System;
using System.Windows;
using Application = System.Windows.Application;

namespace ChatClient
{
    static class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        [STAThread]
        static void Main()
        {
            var loginWindow = new LoginWindow();
            var result = loginWindow.ShowDialog();
            
            if (result != null && result.Value)
            {
                var app = new Application();
                app.ShutdownMode = ShutdownMode.OnMainWindowClose;

                var mianWindow = new MainWindow();
                app.MainWindow = mianWindow;
                app.Run(mianWindow);
            }
        }
    }
}
