using Contracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
       
        
        private static ILogger logger = Log.Logger;
       
        public LoggerManager() 
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console().WriteTo.File("logs/myLogs-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }  
        
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInfo(string message)
        {
            logger.Information(message);
        }
        public void LogWarn(string message)
        {
            logger.Warning(message);
        }

    }
}
