using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using CafeMate.Shared;

namespace CafeMate.Client.Networking
{
    public class ClientSocket
    {
        private readonly string _host;
        private readonly int _port;

        public ClientSocket(string host = "127.0.0.1", int port = 5000)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            try
            {
                using TcpClient client = new TcpClient();
                client.Connect(_host, _port);

                using NetworkStream stream = client.GetStream();
                using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                Console.WriteLine("[CLIENT] Connected to server.");

                // Send test message
                var message = new CafeMate.Shared.Message
                {
                    Type = "Hello",
                    Content = "Hello from client!"
                };
                writer.WriteLine(MessageProtocol.Serialize(message));
                Console.WriteLine($"[CLIENT] Sent: {MessageProtocol.Serialize(message)}");

                // Read server response
                string? responseJson = reader.ReadLine();
                if (!string.IsNullOrEmpty(responseJson))
                {
                    var response = MessageProtocol.Deserialize<CafeMate.Shared.Message>(responseJson);
                    Console.WriteLine($"[CLIENT] Received: {response.Type} - {response.Content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLIENT] Error: {ex.Message}");
            }
        }
    }
}
