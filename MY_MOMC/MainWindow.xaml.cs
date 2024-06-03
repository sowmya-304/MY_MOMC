using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Windows;

namespace MY_MOMC
{
    public partial class MainWindow : Window
    {
        internal List<MY_MOMC.Class.User> users;
        internal static string email;
        internal static int randomDigits;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Page1_Loaded;
        }
        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            OnLoad();
        }
        private void OnLoad()
        {
            string jsonFilePath = "C:\\MY_PROJECT\\MY_MOMC\\MY_MOMC\\data.json";
            string readJsonFile=File.ReadAllText(jsonFilePath);
            users = JsonConvert.DeserializeObject<List<MY_MOMC.Class.User>>(readJsonFile);
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            email = tb_email.Text.Trim();
            try
            {
                if (tb_email.Text.Trim().Length > 0)
                {
                    if (users.Any(user => user.UserEmailId == email))
                    {
                        randomDigits = GenerateRandomNumber(100000, 999999);
                        var result = MessageBox.Show("OTP sent successfully! Are you sure you want to continue with this email ?", "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        if (result == MessageBoxResult.OK)
                        {
                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress(email);
                            mail.To.Add(tb_email.Text.Trim());
                            mail.Subject = "Your OTP";
                            mail.Body = $"Otp is {randomDigits}";

                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                            smtpClient.Port = 587;
                            smtpClient.Credentials = new NetworkCredential(email, "tkki grgd aapo uavx\r\n");
                            smtpClient.EnableSsl = true;
                            smtpClient.Send(mail);

                            MessageBox.Show("Email sent successfully.");
                            VerificationPage vp = new VerificationPage(this);
                            this.Content = vp;
                        }
                        else if (result == MessageBoxResult.Cancel)
                        {
                            tb_email.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Email");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Email");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        internal static int GenerateRandomNumber(int min, int max)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];
                Random random = new Random();
                random.NextBytes(randomNumber);
                int value = BitConverter.ToInt32(randomNumber, 0);
                value = Math.Abs(value);
                int randomRange = max - min + 1;
                int randomOffset = value % randomRange;
                int randomResult = randomOffset + min;
                return randomResult;
            }
        }
        public void NavigateToMainWindow()
        {
            this.Content = MainGrid;
        }
    }
}
