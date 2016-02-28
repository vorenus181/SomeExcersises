using System;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace PersonBook.Data
{
    public class Logger
    {
        public static readonly ILog ServerLog;

        static Logger()
        {
            const string logFilePath = "log";

            if (System.IO.Directory.Exists(logFilePath))
                System.IO.Directory.CreateDirectory(logFilePath);

            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            var appRoller = new RollingFileAppender
            {
                Name = "AppLogClass",
                AppendToFile = true,
                File = @"Logs\AppLog.txt",
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true,
            };

            appRoller.ActivateOptions();
            hierarchy.Root.AddAppender(appRoller);

            var memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;

            ServerLog = LogManager.GetLogger("AppLogClass");
        }

        public static void Error(object message, Exception ex)
        {
            ServerLog.Error(message, ex);
        }

        public static void Error(Exception ex)
        {
            ServerLog.Error(ex);
        }

        public static void Error(object message)
        {
            ServerLog.Error(message);
        }

        public static void ErrorFormat(string message, params object[] parameters)
        {
            ServerLog.ErrorFormat(message, parameters);
        }

        public static void Info(object message)
        {
            ServerLog.Info(message);
        }

        public static void Info(object message, Exception ex)
        {
            ServerLog.Info(message, ex);
        }

        public static void InfoFormat(string message, params object[] parameters)
        {
            ServerLog.InfoFormat(message, parameters);
        }

        public static void Warning(object message)
        {
            ServerLog.Warn(message);
        }

        public static void Warning(object message, Exception ex)
        {
            ServerLog.Warn(message, ex);
        }

        public static void WarningFormat(string message, params object[] parameters)
        {
            ServerLog.WarnFormat(message, parameters);
        }

        public static void DebugFormat(string message, params object[] parameters)
        {
            ServerLog.DebugFormat(message, parameters);
        }
    }
}
