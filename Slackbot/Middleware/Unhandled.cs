using Noobot.Core.MessagingPipeline.Middleware;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using Slackbot.Configuration;
using System;
using System.Collections.Generic;

namespace Slackbot.Middleware
{
    internal class UnhandledMessageMiddleware : IMiddleware
    {
        public UnhandledMessageMiddleware()
        {

        }

        public IEnumerable<ResponseMessage> Invoke(IncomingMessage message)
        {
            if (message.RawText.StartsWith("."))
            {
                var parsedCommand = message.RawText.Remove(0, 1);
                var urlEncoded = Uri.EscapeDataString(parsedCommand);
                var request = Slackbot.Client.GetAsync($"?p={Settings.SitePass}&u={message.Username}&ch={message.Channel}&c={urlEncoded}").Result;
                var response = request.Content.ReadAsStringAsync().Result.Clean();

                var splitResponse = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in splitResponse)
                {
                    if (response.StartsWith("#PM#")) yield return message.ReplyDirectlyToUser(line.Replace("#PM#", ""));
                    else yield return message.ReplyToChannel(line);
                }
            }
            else
            {
                Slackbot.Client.GetAsync($"?p={Settings.SitePass}&u={message.Username}&u2={message.Username}&ch={message.Channel.Replace("#", "")}&c=log&msg={message.RawText}&users={message.Username}");
            }
        }

        public IEnumerable<CommandDescription> GetSupportedCommands() => new CommandDescription[0];
    }
}