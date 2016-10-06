using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Einfache File-IO zur Verwaltung eines Chats
    /// Die Chat-Historie wird in einer Textdatei zwischengespeichert und bei Bedarf wieder ausgelesen
    /// </summary>
    class ChatHandler
    {
        private List<String> chat;
        private string chatPath = $"C:\\projects\\projekt16\\Server\\chat\\chatlog.txt";

        public ChatHandler()
        {
            chat = GetChatFromFile();
        }

        /// <summary>
        /// Lade den Chatverlauf aus der Datei
        /// </summary>
        /// <returns>Chatverlauf als Liste von Strings</returns>
        public List<String> GetChatFromFile()
        {
            List<String> chatlog = new List<string>();
            if (File.Exists(chatPath))
            {
                chatlog = new List<string>(File.ReadAllLines(chatPath));
            }
            return chatlog;
        }

        /// <summary>
        /// Fügt eine neue Nachricht zum Chat hinzu
        /// </summary>
        /// <param name="user">Absendername</param>
        /// <param name="message">Nachricht</param>
        public void AddMessage(string user, string message)
        {
            chat.Add($"{DateTime.Now}|{user}: {message}");
            SaveChatToFile();
        }

        /// <summary>
        /// Speichert den Chat in die Datei
        /// </summary>
        public void SaveChatToFile()
        {
            File.WriteAllLines(chatPath, chat.ToArray());
        }

        /// <summary>
        /// Gibt den Chat zurück
        /// </summary>
        /// <returns>Chat als Liste von Strings</returns>
        public List<String> GetChat()
        {
            return this.chat;
        }
    }
}
