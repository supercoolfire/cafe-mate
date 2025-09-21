using System.Text.Json;

namespace CafeMate.Shared
{
    public class Message
    {
        public string Type { get; set; }   // e.g. "Chat", "Login", "Status"
        public string Data { get; set; }   // payload text
    }

    public static class MessageProtocol
    {
        public static string Serialize(Message msg)
        {
            return JsonSerializer.Serialize(msg);
        }

        public static Message Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }
    }
}