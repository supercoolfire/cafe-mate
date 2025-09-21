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

            string responseString = "";
            Message receivedMessage = MessageProtocol.Deserialize(msg);

            switch (receivedMessage.Type)
            {
                case "LOGIN_REQUEST":
                    Console.WriteLine("[SERVER] Received LOGIN_REQUEST: {msg}");
                    responseString = $"ACK for LOGIN_REQUEST {receivedMessage.RequestId}";
                    break;
                default:
                    Console.WriteLine($"[SERVER] Received unknown message type: {receivedMessage.Type}: {msg}");
                    responseString = "UNKNOWN MESSAGE TYPE";
                    break;
            }

            Console.WriteLine($"[SERVER] Sending: {responseString}");

            byte[] response = Encoding.UTF8.GetBytes(responseString);
            stream.Write(response, 0, response.Length);
} catch (Exception ex) {
Console.WriteLine($"[SERVER] Error: {ex.Message}");
}
}
}
}