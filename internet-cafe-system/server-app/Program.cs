using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
            byte[] buffer = Encoding.UTF8.GetBytes("Hello from server!");
            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }
    }
}
