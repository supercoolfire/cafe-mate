// server-app/Networking/ServerSocket.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


class ServerSocket
{
public void Start()
{
TcpListener server = new TcpListener(IPAddress.Any, 5000);
server.Start();
Console.WriteLine("[SERVER] Listening on port 5000...");


while (true)
{
TcpClient client = server.AcceptTcpClient();
try {
NetworkStream stream = client.GetStream();


byte[] buffer = new byte[1024];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);


Console.WriteLine($"[CLIENT] {msg}");


            byte[] response = Encoding.UTF8.GetBytes("HELLO FROM SERVER");
            stream.Write(response, 0, response.Length);
} catch (Exception ex) {
Console.WriteLine($"[SERVER] Error: {ex.Message}");
}
}
}
}