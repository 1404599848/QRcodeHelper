using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRcodeHelper
{
    public class NLogHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        #region Debug

        public static void Debug(string msg, params object[] args)
        {
            logger.Debug(msg, args);
        }

        public static void Debug(string msg, Exception err)
        {
            logger.Debug(err, msg);
        }

        public static void Debug(Exception err)
        {
            logger.Debug(err);
        }

        #endregion

        #region Info

        public static void Info(string msg, params object[] args)
        {
            logger.Info(msg, args);
        }

        public static void Info(string msg, Exception err)
        {
            logger.Info(err, msg);
        }

        public static void Info(Exception err)
        {
            logger.Info(err);
        }

        #endregion

        #region Warn

        public static void Warn(string msg, params object[] args)
        {
            logger.Warn(msg, args);
        }

        public static void Warn(string msg, Exception err)
        {
            logger.Warn(err, msg);
        }
        public static void Warn(Exception err)
        {
            logger.Warn(err);
        }

        #endregion

        #region Trace

        public static void Trace(string msg, params object[] args)
        {
            logger.Trace(msg, args);
        }

        public static void Trace(string msg, Exception err)
        {
            logger.Trace(err, msg);
        }

        #endregion

        #region Error

        public static void Error(string msg, params object[] args)
        {
            logger.Error(msg, args);
        }

        public static void Error(Exception err)
        {
            logger.Error(err);
        }

        public static void Error(string msg, Exception err)
        {
            logger.Error(err, msg);
        }

        #endregion

        #region Fatal

        public static void Fatal(string msg, params object[] args)
        {
            logger.Fatal(msg, args);
        }

        public static void Fatal(string msg, Exception err)
        {
            logger.Fatal(err, msg);
        }

        #endregion
    }
}
