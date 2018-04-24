using Noobot.Core.Configuration;
using Slackbot.Middleware;
using System;

namespace Slackbot.Configuration
{
    public class Config : ConfigurationBase
    {
        public Config()
        {
            UseMiddleware<UnhandledMessageMiddleware>();
        }
    }

    public class SlackConfiguration : IConfigReader
    {
        public bool HelpEnabled => false;
        public bool StatsEnabled => false;
        public bool AboutEnabled => false;
        public string SlackApiKey => Settings.SlackApiKey;

        public T GetConfigEntry<T>(string entryName)
        {
            throw new NotImplementedException();
        }
    }
}