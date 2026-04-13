using System;
using System.Collections.Generic;
using System.Text;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using System.Net.Mail;

namespace ErrorReportLibrary.Implementation
{
    public class EmailErrorLogger : IErrorLogger
    {
        private readonly IMailSender _mailSender;
        private string SmtpServer ="smtp.gmail.com";
        private int SmtpPort = 587;
        private string FromEmail = "tejas.microlise10@gmail.com";
        private string ToEmail ;
        //private string UserName = "tejas.microlise10@gmail.com";
        private string Password = "";


        public EmailErrorLogger(string toEmail)
        {
            ToEmail = toEmail;
        }

        // Simple constructor for dependency injection of a mail sender (used in tests)
        public EmailErrorLogger(string toEmail, IMailSender mailSender)
        {
            ToEmail = toEmail;
            _mailSender = mailSender;
        }
        public EmailErrorLogger(string smtpServer, int smtpPort, string toEmail)
        {
            SmtpServer = smtpServer;
            SmtpPort = smtpPort;
            ToEmail = toEmail;
        }


        public EmailErrorLogger(string smtpServer, int smtpPort, string fromEmail, string password, string toEmail)
        {
            SmtpServer = smtpServer;
            SmtpPort = smtpPort;
            FromEmail = fromEmail;
            Password = password;
            ToEmail = toEmail;
            //UserName = userName;
        }
        public void LogError(ErrorDetails error)
        {
            StringBuilder LogMessage = new StringBuilder();
            LogMessage.AppendLine($"Error Code: {error.ErrorCode}");
            LogMessage.AppendLine($"Title: {error.Title}");
            LogMessage.AppendLine($"Description: {error.Description}");
            LogMessage.AppendLine($"Help URL: {error.HelpUrl}");
            //Console.WriteLine(LogMessage.ToString());
            // Compose the message
            var mail = new MailMessage(FromEmail, ToEmail)
            {
                Subject = $"Error Report: {error.Title}",
                Body = LogMessage.ToString()
            };

            // If a mail sender is provided (e.g. a test fake), use it. Otherwise send via SmtpClient.
            if (_mailSender != null)
            {
                _mailSender.Send(mail);
                return;
            }

            // Default behavior: send using SmtpClient (keeps backwards compatibility).
            var smtp = new SmtpClient(SmtpServer, SmtpPort)
            {
                Credentials = new System.Net.NetworkCredential(FromEmail, Password),
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }
}
