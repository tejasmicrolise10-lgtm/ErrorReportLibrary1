using System.Net.Mail;

namespace ErrorReportLibrary.Interface
{
    public interface IMailSender
    {
        void Send(MailMessage message);
    }
}
