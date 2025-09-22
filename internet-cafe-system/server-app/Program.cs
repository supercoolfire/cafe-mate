using System.Net;
using System.Net.Sockets;
using System.Text;
using CafeMate.Shared; // shared project for MessageProtocol

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("[Server] Listening on port 5000...");

            using var client = listener.AcceptTcpClient();
            Console.WriteLine("[Server] Client connected!");

            using var stream = client.GetStream();
            using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            // Serialize and send message
            var message = new Message { Type = "ServerGreeting", Content = "Hello from server!" };
            string json = MessageProtocol.Serialize(message);

            writer.WriteLine(json);
            Console.WriteLine($"[Server] Sent: {json}");
        }
    }
}
