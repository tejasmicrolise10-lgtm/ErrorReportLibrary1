using System.Net.Mail;
using ErrorReportLibrary.Interface;

namespace ErrorReportLibrary.Unit.Test.TestDoubles
{
    public class FakeMailSender : IMailSender
    {
        public MailMessage SentMessage { get; private set; }

        public void Send(MailMessage message)
        {
            SentMessage = message;
        }
    }
}
