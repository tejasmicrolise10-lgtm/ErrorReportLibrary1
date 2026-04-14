using System;
using System.Collections.Generic;
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
        private string FromEmail = "tp1004project@gmail.com";
        private string ToEmail ;
        private string Password = "tyzl ntjk liho rtjl";


        public EmailErrorLogger(string toEmail)
        {
            ToEmail = toEmail;
        }

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
        }
        public void LogError(ErrorDetails error)
        {
            var mail = new MailMessage(FromEmail, ToEmail)
            {
                Subject = $"Error Report: {error.Title}",
                Body = error.FormatErrorMessage()
            };


            if (_mailSender != null)
            {
                _mailSender.Send(mail);
                return;
            }

            var smtp = new SmtpClient(SmtpServer, SmtpPort)
            {
                Credentials = new System.Net.NetworkCredential(FromEmail, Password),
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }
}
