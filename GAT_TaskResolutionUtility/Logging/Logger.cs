using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAT_TaskResolutionUtility.Logging
{
    public class Logger : ILogger
    {
        private static readonly Logger _instance = new Logger();
        protected ILog monitoringLogger;
        protected static ILog debugLogger;

        public Logger()
        {
            monitoringLogger = LogManager.GetLogger("MonitoringLogger");
            debugLogger = LogManager.GetLogger("DebugLogger");
        }


        public void LogDebug(string message)
        {
            debugLogger.Debug(message);
        }

        public void LogError(string message)
        {
            _instance.monitoringLogger.Error(message);
        }

        public void LogFatal(string message)
        {
            _instance.monitoringLogger.Fatal(message);
        }

        public void LogInfo(string message)
        {
            _instance.monitoringLogger.Info(message);
        }

        public void LogWarning(string message)
        {
            _instance.monitoringLogger.Warn(message);
        }
    }
}