using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using CafeMate.Shared;

namespace server_app
{
    class Server
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

                _ = HandleClientAsync(client); // fire-and-forget task
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                // Send greeting
                var greeting = new CafeMate.Shared.Message
                {
                    Type = "ServerGreeting",
                    Content = "Hello from server!"
                };
                string greetingJson = MessageProtocol.Serialize(greeting);
                await writer.WriteLineAsync(greetingJson);
                Console.WriteLine($"[Server] Sent: {greetingJson}");

                // Now keep listening for messages
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var message = MessageProtocol.Deserialize<CafeMate.Shared.Message>(line);
                    Console.WriteLine($"[Server] Received: {message?.Content}");

                    // Reply back
                    var ack = new CafeMate.Shared.Message
                    {
                        Type = "Ack",
                        Content = $"Received {message?.Content}"
                    };
                    string ackJson = MessageProtocol.Serialize(ack);
                    await writer.WriteLineAsync(ackJson);
                    Console.WriteLine($"[Server] Sent: {ackJson}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Server] Error: {ex.Message}");
            }
        }
    }
}
