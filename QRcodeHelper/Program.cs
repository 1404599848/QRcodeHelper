using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRcodeHelper
{
    static class Program
    {
        private const string MutexName = "QRcodeHelper";
        private static Mutex Mutex;


        [STAThread]
        static void Main()
        {
            try
            {
                NLogHelper.Info("启动");
                using (Mutex = new Mutex(false, MutexName, out bool mutexCreated))
                {
                    //程序已在运行
                    if (!mutexCreated)
                    {
                        NLogHelper.Info("已有示例在运行");
                        Environment.Exit(0);
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //设置应用程序处理异常方式：ThreadException处理
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //处理UI线程异常
                    Application.ThreadException += Application_ThreadException;
                    //处理非UI线程异常
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                    Application.Run(new MainForm());
                }

            }
            catch (Exception ex)
            {
                NLogHelper.Error(ex);
            }

        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            NLogHelper.Error(e.Exception);
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            NLogHelper.Error(e.ExceptionObject as Exception);
        }
    }
}
