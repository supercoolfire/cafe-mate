using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using CafeMate.Shared;

namespace client_app;

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

            // Read server greeting
            string? greetingJson = reader.ReadLine();
            if (greetingJson != null)
            {
                var greeting = MessageProtocol.Deserialize<CafeMate.Shared.Message>(greetingJson);
                Console.WriteLine($"[CLIENT] Received: {greeting.Type} - {greeting.Content}");
            }

            // Send a test message
            var msg = new CafeMate.Shared.Message { Type = "Hello", Content = "Hello from client!" };
            writer.WriteLine(MessageProtocol.Serialize(msg));
            Console.WriteLine("[CLIENT] Sent: Hello");

            // Wait for ACK
            string? ackJson = reader.ReadLine();
            if (ackJson != null)
            {
                var ack = MessageProtocol.Deserialize<CafeMate.Shared.Message>(ackJson);
                Console.WriteLine($"[CLIENT] Received ACK: {ack.Content}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CLIENT] Error: {ex.Message}");
        }
    }
}
