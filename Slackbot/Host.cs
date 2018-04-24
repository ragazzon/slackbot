using Common.Logging;
using Noobot.Core;
using Noobot.Core.Configuration;
using Noobot.Core.DependencyResolution;
using Slackbot.Configuration;
using Slackbot.Middleware;
using System;

namespace Slackbot
{
    public class BotHost
    {
        private readonly IConfigReader _configReader;
        private INoobotCore _noobotCore;
        private readonly IConfiguration _configuration;

        public BotHost(IConfigReader configReader)
        {
            _configReader = configReader;
            _configuration = new Config();
        }

        public void Start()
        {
            IContainerFactory containerFactory = new ContainerFactory(_configuration, _configReader, LogManager.GetLogger(GetType()));
            INoobotContainer container = containerFactory.CreateContainer();
            _noobotCore = container.GetNoobotCore();

            Console.WriteLine("Connecting...");
            _noobotCore
                .Connect()
                .ContinueWith(task =>
                {
                    if (!task.IsCompleted || task.IsFaulted)
                    {
                        Console.WriteLine($"Error connecting to Slack: {task.Exception}");
                    }

                    Settings.ChannelList = _noobotCore.ListChannels();

                    var lastRun = DateTime.UtcNow;
                    while (true)
                    {
                        if (lastRun.AddSeconds(1) <= DateTime.UtcNow)
                        {
                            var check = new MunkCheck();
                            check.CheckForEvents(_noobotCore);

                            lastRun = DateTime.UtcNow;
                        }
                    }
                })
                .Wait();
        }

        public void Stop()
        {
            Console.WriteLine("Disconnecting...");
            _noobotCore?.Disconnect();
        }
    }
}