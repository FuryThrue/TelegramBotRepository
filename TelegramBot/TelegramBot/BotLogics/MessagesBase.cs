using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TelegramBot.BotLogics
{
    [Serializable]
    public class MessagesBase
    {
        public List<string> MessagesList;

        public void Deserialize()
        {
            var fileName = "answers_base.xml";
            var currentPath = Directory.GetCurrentDirectory();
            var fullPath = currentPath + "\\" + fileName;

            var fs = new FileStream(fullPath, FileMode.Open);
            var reader = XmlReader.Create(fs);

            var serializer = new XmlSerializer(typeof(MessagesBase));
            var messagesBase = (MessagesBase)serializer.Deserialize(reader);
            this.MessagesList = messagesBase.MessagesList;
        }
    }
}
