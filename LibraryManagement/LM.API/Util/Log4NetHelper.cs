using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml;
using System.IO;

namespace LM.API.Util
{
    public static class Log4NetHelper
    {
        public static IHostBuilder ConfigureLog4Net(this IHostBuilder webHost)
        {
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead("log4net.config"));

            var loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(Hierarchy));

            XmlConfigurator.Configure(loggerRepository, log4NetConfig["log4net"]);

            webHost.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddLog4Net();
            });

            return webHost;
        }
    }
}
