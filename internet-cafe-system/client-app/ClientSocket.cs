// client-app/Networking/ClientSocket.cs
using System;
using System.Net.Sockets;
using System.Text;

namespace client_app;

class ClientSocket
{
public void Connect()
{
try
{
TcpClient client = new TcpClient("127.0.0.1", 5000);
NetworkStream stream = client.GetStream();


byte[] message = Encoding.UTF8.GetBytes("HELLO FROM CLIENT");
stream.Write(message, 0, message.Length);


byte[] buffer = new byte[1024];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);


Console.WriteLine($"[SERVER] {response}");
}
catch (Exception ex)
{
Console.WriteLine($"[CLIENT] Error: {ex.Message}");
}
}
}