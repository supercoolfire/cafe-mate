// client-app/Program.cs
using System;
using CafeMate.Client.Networking; // Namespace where ClientSocket is defined

namespace client_app
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("[CLIENT] Starting...");

            // Create and connect the client socket
            ClientSocket client = new ClientSocket();
            client.Connect();

            Console.WriteLine("[CLIENT] Finished.");
        }
    }
}
