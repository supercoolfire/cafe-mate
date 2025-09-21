using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using CafeMate.Shared;

class Program
{
    static void Main()
    {
        Console.WriteLine("[Server] Starting...");

        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("[Server] Listening on port 5000...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("[Server] Client connected!");

            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            // Sending to client
            var msg = new Message { Type = "Chat", Data = "Hello from server!" };
            string json = MessageProtocol.Serialize(msg);
            writer.WriteLine(json);


            // Receiving from client
            string line = reader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                var msg = MessageProtocol.Deserialize(line);
                Console.WriteLine($"[Client -> Server] {msg.Type}: {msg.Data}");
            }

            client.Close();
        }
    }
}