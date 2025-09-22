using System.Net.Sockets;
using System.Text;
using System.Text.Json;

TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 5000);
listener.Start();
Console.WriteLine("[Server] Listening on port 5000...");

while (true)
{
    TcpClient client = listener.AcceptTcpClient();
    Console.WriteLine("[Server] Client connected!");

    _ = Task.Run(() =>
    {
        using NetworkStream stream = client.GetStream();

        // Send greeting
        var greeting = new { Type = "ServerGreeting", Content = "Hello from server!" };
        string json = JsonSerializer.Serialize(greeting);
        byte[] buffer = Encoding.UTF8.GetBytes(json + "\n");
        stream.Write(buffer, 0, buffer.Length);
        Console.WriteLine("[Server] Sent: " + json);

        // 🔑 Keep listening for client messages
        using var reader = new StreamReader(stream, Encoding.UTF8);
        while (true)
        {
            string? line = reader.ReadLine();
            if (line == null) break; // client disconnected
            Console.WriteLine("[Server] Received: " + line);
        }
    });
}
