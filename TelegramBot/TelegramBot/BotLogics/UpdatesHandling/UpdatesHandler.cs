using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TelegramBot.BotLogics.UpdatesHandling
{
    class UpdatesHandler
    {
        private string _message = "message";
        //private string _editedЬessage = "edited_message";
        //private string _channelPost = "channel_post";
        //private string _editedСhannelPost = "edited_channel_post";
        private MessagesBase _messagesBase;

        public UpdatesHandler()
        {
            _messagesBase = new MessagesBase();
            _messagesBase.Deserialize();
        }

        public string GetAvailableParameters()
        {
            return $"allowed_updates={_message}";//,{_editedЬessage},{_channelPost},{_editedСhannelPost}";
        }

        public void UpdatesHandle(JToken updates)
        {
            foreach (var update in updates["result"])
            {
                if (update[_message] != null)
                {
                    MessageHandle(update[_message]);
                    break;
                }
                //if (update[_editedЬessage] != null)
                //{
                //    //handle
                //    break;
                //}
                //if (update[_channelPost] != null)
                //{
                //    //handle
                //    break;
                //}
                //if (update[_editedСhannelPost] != null)
                //{
                //    //handle
                //    break;
                //}
            }
        }

        private async void MessageHandle(JToken message)
        {
            var dictionary = new Dictionary<int, int>();

            var chatId = (int)message["chat"]["id"];
            var messageText = (string)message["text"];
            var messageWords = messageText.Split();
            var maxPriority = 0;
            for (int i = 0; i < _messagesBase.MessagesList.Count; i++)
            {
                var currentMessage = _messagesBase.MessagesList[i];
                var priority = 0;
                foreach (var word in messageWords)
                {
                    if (currentMessage.Contains(word))
                    {
                        priority++;
                    }
                }
                if (maxPriority < priority)
                {
                    dictionary.Clear();
                    dictionary.Add(dictionary.Count, i);
                    maxPriority = priority;
                }
                if (maxPriority == priority)
                {
                    dictionary.Add(dictionary.Count, i);
                }
            }
            var resultAnswer = "";
            if (dictionary.Count > 0)
            {
                var resultIndex = new Random().Next(0, dictionary.Count);
                resultAnswer = _messagesBase.MessagesList[dictionary[resultIndex] + new Random().Next(0,dictionary.Count - resultIndex)];
            }
            else
            {
                resultAnswer = "мне нечего сказать(";
            }

            await CustomHttpClient.MakeRequest("sendMessage", $"chat_id={chatId}&text={resultAnswer}");
        }
    }
}
