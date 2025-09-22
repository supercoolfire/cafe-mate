using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using CafeMate.Shared;

namespace CafeMate.Server.Networking
{
    public class ServerSocket
    {
        private TcpListener? _server;

        public void Start(int port = 5000)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            Console.WriteLine($"[SERVER] Listening on port {port}...");

            while (true)
            {
                try
                {
                    using TcpClient client = _server.AcceptTcpClient();
                    using NetworkStream stream = client.GetStream();
                    using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                    Console.WriteLine("[SERVER] Client connected!");

                    // Read client message
                    string? msgJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(msgJson))
                        continue;

                    var receivedMessage = MessageProtocol.Deserialize<Message>(msgJson);
                    Console.WriteLine($"[CLIENT MSG] Type={receivedMessage.Type}, Content={receivedMessage.Content}");

                    // Send server greeting
                    var response = new Message
                    {
                        Type = "ServerGreeting",
                        Content = "Hello from server!"
                    };
                    writer.WriteLine(MessageProtocol.Serialize(response));
                    Console.WriteLine($"[SERVER] Sent: {MessageProtocol.Serialize(response)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SERVER] Error: {ex.Message}");
                }
            }
        }

        public void Stop()
        {
            _server?.Stop();
            Console.WriteLine("[SERVER] Stopped.");
        }
    }
}
