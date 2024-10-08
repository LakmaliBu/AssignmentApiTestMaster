using AssignmentApiTestMaster.Config;
using log4net;


namespace AssignmentApiTestMaster.Utilities
{
    public static class Logger
    {
        static readonly ILog _logger = LogManager.GetLogger(typeof(PropertyFileReader));
        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        public static void LogInfo(string message)
        {
            _logger.Info(message);

        }
    }

}
