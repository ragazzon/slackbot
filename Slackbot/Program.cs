using Slackbot.Configuration;
using System;
using System.Net.Http;

namespace Slackbot
{
    public class Slackbot
    {
        public static HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri("http://" + Settings.MunkSite + ".umunk.net/ircbot/bot.php")
        };

        static void Main(string[] args)
        {
            try
            {
                var bot = new BotHost(new SlackConfiguration());
                bot.Start();
            }
            catch
            {
                Main(new string[0]);
            }
        }
    }
}