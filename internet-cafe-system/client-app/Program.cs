using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using CafeMate.Shared;

namespace client_app;

class Program
{
    static void Main()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            // Sending to server
            var msg = new Message { Type = "Login", Data = "Client123" };
            writer.WriteLine(MessageProtocol.Serialize(msg));

            // Receiving from server
            string line = reader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                var msg = MessageProtocol.Deserialize(line);
                Console.WriteLine($"[Server -> Client] {msg.Type}: {msg.Data}");
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CLIENT] Error: {ex.Message}");
        }
    }
}