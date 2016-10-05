using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ChatHandler
    {
        private List<String> chat;
        private string chatPath = $"C:\\projects\\projekt16\\Server\\chat\\chatlog.txt";

        public ChatHandler()
        {
            chat = GetChatFromFile();
        }

        public List<String> GetChatFromFile()
        {
            List<String> chatlog = new List<string>();
            if (File.Exists(chatPath))
            {
                chatlog = new List<string>(File.ReadAllLines(chatPath));
            }
            return chatlog;
        }

        public void AddMessage(string user, string message)
        {
            chat.Add($"{DateTime.Now}|{user}: {message}");
            SaveChatToFile();
        }

        public void SaveChatToFile()
        {
            File.WriteAllLines(chatPath, chat.ToArray());
        }

        public List<String> GetChat()
        {
            return this.chat;
        }
    }
}
