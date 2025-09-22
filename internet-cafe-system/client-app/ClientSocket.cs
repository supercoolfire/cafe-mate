using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using CafeMate.Shared;

namespace client_app
{
    class ClientSocket
    {
        public void Connect()
        {
            try
            {
                using TcpClient client = new TcpClient("127.0.0.1", 5000);
                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                Console.WriteLine("[CLIENT] Connected to server.");

                // Send a test message
                var message = new CafeMate.Shared.Message { Type = "Hello", Content = "Hello from client!" };
                writer.WriteLine(MessageProtocol.Serialize(message));

                // Read server response
                string? responseJson = reader.ReadLine();
                if (!string.IsNullOrEmpty(responseJson))
                {
                    var response = MessageProtocol.Deserialize<CafeMate.Shared.Message>(responseJson);
                    Console.WriteLine($"[CLIENT] Received: {response?.Content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLIENT] Error: {ex.Message}");
            }
        }
    }
}
