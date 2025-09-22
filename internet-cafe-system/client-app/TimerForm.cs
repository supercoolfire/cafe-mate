using System;
using System.Windows.Forms;

public class TimerForm : Form
{
    private Label lblTime;
    private System.Windows.Forms.Timer countdownTimer;
    private int remainingSeconds = 10; // example 10 seconds

    public TimerForm()
    {
        lblTime = new Label
        {
            Text = "Time: 10",
            Font = new System.Drawing.Font("Arial", 16),
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };
        Controls.Add(lblTime);

        countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 }; // 1 second
        countdownTimer.Tick += CountdownTimer_Tick;
        countdownTimer.Start();
    }

    private void CountdownTimer_Tick(object? sender, EventArgs e)
    {
        remainingSeconds--;
        lblTime.Text = $"Time: {remainingSeconds}";

        if (remainingSeconds <= 0)
        {
            countdownTimer.Stop();
            MessageBox.Show("Session ended!");
            Close();
        }
    }
}
