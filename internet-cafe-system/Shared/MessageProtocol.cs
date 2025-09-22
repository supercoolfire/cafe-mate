using System.Text.Json;

namespace CafeMate.Shared
{
    public class Message
    {
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public static class MessageProtocol
    {
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json)!;
        }
    }
}
