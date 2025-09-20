using System;

namespace client_app;

class Program
{
    static void Main()
    {
        try
        {
            ClientSocket client = new ClientSocket();
            client.Connect();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CLIENT] Error: {ex.Message}");
        }
    }
}