using System;
using System.Windows.Forms;

namespace client_app.Forms;

public partial class LoginForm : Form
{
    public LoginForm()
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
