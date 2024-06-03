using System;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;

namespace MY_MOMC
{
    public partial class VerificationPage : UserControl
    {
        private DispatcherTimer timer;
        private TimeSpan time;
        private MainWindow _mainWindow;
        public VerificationPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            this.Loaded += Page2_Loaded;
        }
        private void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            heading.Text = $"We've Sent an OTP to your email - {MY_MOMC.MainWindow.email}";
            btn_verifyotp.IsEnabled = true;
            btn_ResendOTP.IsEnabled = false;
            btn_ResendOTP.Foreground = Brushes.Gray;
            StartCountdownTimer();
        }
        private void StartCountdownTimer()
        {
            time = TimeSpan.FromMinutes(1);
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += Timer_Tick;

            timer.Start();
            lbl_timer.Text = $" Time Left : {time.ToString("mm\\:ss")}";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time.TotalSeconds > 0)
            {
                time = time.Add(TimeSpan.FromSeconds(-1));
                lbl_timer.Text = $" Time Left : {time.ToString("mm\\:ss")}";
            }
            else
            {
                btn_verifyotp.IsEnabled = false;
                btn_verifyotp.Foreground = Brushes.Gray;
                btn_ResendOTP.IsEnabled = true;
                timer.Stop();
            }
        }
        private void VerifyOTP_Click(object sender, RoutedEventArgs e)
        {
            if (tb_otp.Text == MY_MOMC.MainWindow.randomDigits.ToString())
            {
                MeetingInfo mf = new MeetingInfo();
                _mainWindow.Content = mf;
            }
        }
        private void ResendOTP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MY_MOMC.MainWindow.randomDigits = MY_MOMC.MainWindow.GenerateRandomNumber(100000, 999999);
                var result = MessageBox.Show("OTP sent successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("19n81a05c9.sowmya@gmail.com");
                mail.To.Add(MY_MOMC.MainWindow.email);
                mail.Subject = "Your OTP";
                mail.Body = $"Otp is {MY_MOMC.MainWindow.randomDigits}";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("19n81a05c9.sowmya@gmail.com", "tkki grgd aapo uavx\r\n");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                MessageBox.Show("Email sent successfully.");
                btn_ResendOTP.IsEnabled = false;
                btn_ResendOTP.Foreground = Brushes.Gray;
                btn_verifyotp.IsEnabled = true;
                StartCountdownTimer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        } 
        private void NavigateToMainWindow(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToMainWindow();
        }
    }
}
