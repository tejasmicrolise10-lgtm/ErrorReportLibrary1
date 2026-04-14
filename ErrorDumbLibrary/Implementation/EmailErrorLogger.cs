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
    private readonly string _fromEmail;
    private readonly string _toEmail;

    public EmailErrorLogger(
        string fromEmail,
        string toEmail,
        IMailSender mailSender)
    {
        _fromEmail = fromEmail;
        _toEmail = toEmail;
        _mailSender = mailSender;
    }

    public void LogError(ErrorDetails error)
    {
        if (error == null)
            throw new ArgumentNullException(nameof(error));

        var body = new StringBuilder()
            .AppendLine($"Error Code: {error.ErrorCode}")
            .AppendLine($"Title: {error.Title}")
            .AppendLine($"Description: {error.Description}")
            .AppendLine($"Help URL: {error.HelpUrl}")
            .ToString();

        var mail = new MailMessage(_fromEmail, _toEmail)
        {
            Subject = $"Error Report: {error.Title}",
            Body = body
        };

        _mailSender.Send(mail);
    }
}
}
