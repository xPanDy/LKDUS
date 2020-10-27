using LKDUS_API.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        void ILoggerService.LogDebug(string message)
        {

            logger.Debug(message);
            
            //throw new NotImplementedException();
        }

        void ILoggerService.LogError(string message)
        {
            // throw new NotImplementedException();
            logger.Error(message);

        }

        void ILoggerService.LogInfo(string message)
        {
            // throw new NotImplementedException();
            logger.Info(message);

        }

        void ILoggerService.LogWarn(string message)
        {
            // throw new NotImplementedException();
            logger.Warn(message);

        }
    }
}
