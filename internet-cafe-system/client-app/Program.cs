// client-app\Program.cs
using System;
using System.Net.Sockets;
using System.Text;
using CafeMate.Shared;

namespace client_app // <-- add this so it matches
{
    class Program
    {
        static void Main()
        {
            var client = new ClientSocket();
            client.Connect();
        }
    }
}
