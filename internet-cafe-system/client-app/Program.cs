// client-app/Program.cs
using System;
using CafeMate.Client.Networking; // Namespace where ClientSocket is defined

class Program
{
    static void Main()
    {
        Console.WriteLine("[CLIENT] Starting...");

        // Create and connect the client socket
        var client = new ClientSocket();
        client.Connect();

        Console.WriteLine("[CLIENT] Finished.");
    }
}
