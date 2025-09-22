using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using CafeMate.Shared;

namespace server_app
{
    public class Server
    {
        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("[Server] Listening on port 5000...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("[Server] Client connected!");
                _ = HandleClientAsync(client); // run client handler
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using var stream = client.GetStream();
                using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                using var reader = new StreamReader(stream, Encoding.UTF8);

                // send greeting
                var greeting = new Message { Type = "ServerGreeting", Content = "Hello from server!" };
                await writer.WriteLineAsync(MessageProtocol.Serialize(greeting));
                Console.WriteLine("[Server] Sent greeting");

                // stay alive and listen
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var msg = MessageProtocol.Deserialize<Message>(line);
                    Console.WriteLine($"[Server] Received: {msg?.Content}");

                    var ack = new Message { Type = "Ack", Content = $"Got {msg?.Content}" };
                    await writer.WriteLineAsync(MessageProtocol.Serialize(ack));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Server] Error: {ex.Message}");
            }
        }
    }
}
