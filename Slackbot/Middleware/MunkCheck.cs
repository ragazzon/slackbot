using Noobot.Core;
using Noobot.Core.MessagingPipeline.Response;
using Slackbot.Configuration;
using System;
using System.Linq;

namespace Slackbot.Middleware
{
    public class MunkCheck
    {
        public async void CheckForEvents(INoobotCore _core)
        {
            var request = Slackbot.Client.GetAsync($"?p={Settings.SitePass}&u=bot&c=ping").Result;
            var response = request.Content.ReadAsStringAsync().Result.Clean();
            var splitResponse = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var text in splitResponse)
            {
                var message = ConstructMessage(_core, text);
                await _core.SendMessage(message).ContinueWith(task => { });
            }
        }

        public ResponseMessage ConstructMessage(INoobotCore _core, string textToSend)
        {
            var toCustomChannel = textToSend.Contains("#PM#");

            if (!toCustomChannel)
            {
                return new ResponseMessage()
                {
                    ResponseType = ResponseType.Channel,
                    Text = textToSend,
                    Channel = Settings.ChannelList.FirstOrDefault(e => e.Value == Settings.DefaultChannel).Key,
                };
            }
                
            textToSend = textToSend.Replace("#PM#", "");
            var index = textToSend.IndexOf("%&%");
            var target = textToSend.Substring(0, index);
            textToSend = textToSend.Replace("%&%", "").Replace(target, "");

            var message = new ResponseMessage()
            {
                ResponseType = ResponseType.Channel,
                Text = textToSend
            };

            var channelId = Settings.ChannelList.FirstOrDefault(e => e.Value == target).Key;
            if (channelId == null)
            {
                var userId = _core.GetUserIdForUsername(target);
                message.UserId = userId;
                message.ResponseType = ResponseType.DirectMessage;
            }
            else
            {
                message.Channel = channelId;
            }
            return message;
        }
    }
}
