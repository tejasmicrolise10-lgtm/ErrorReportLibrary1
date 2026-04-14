using System.Net;
using System.Net.Mail;
using ErrorReportLibrary.Interface;

namespace ErrorReportLibrary.Implementation
{
    public class SmtpMailSender : IMailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _password;

       
        public SmtpMailSender(
            string smtpServer,
            int smtpPort,
            string fromEmail,
            string password)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _fromEmail = fromEmail;
            _password = password;
        }

        public void Send(MailMessage message)
        {
            var smtp = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_fromEmail, _password),
                EnableSsl = true
            };

            smtp.Send(message);
        }
    }
}