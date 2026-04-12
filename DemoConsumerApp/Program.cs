using System;
using ErrorReportLibrary.Implementation;
using ErrorReportLibrary.Model;

// Demo: send a test error email using EmailErrorLogger.
// Configure these environment variables before running:
// SMTP_SERVER, SMTP_PORT, FROM_EMAIL, EMAIL_PASSWORD, TO_EMAIL

var smtpServer =  "smtp.gmail.com";
var smtpPortStr = "587";
var fromEmail = "harsh2504patil@gmail.com";
var password = "";
var toEmail = "harshvardhan.microlise@gmail.com";

if (string.IsNullOrWhiteSpace(smtpServer) || string.IsNullOrWhiteSpace(smtpPortStr)
    || string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(password)
    || string.IsNullOrWhiteSpace(toEmail))
{
    Console.WriteLine("Missing one or more required environment variables:");
    Console.WriteLine("SMTP_SERVER, SMTP_PORT, FROM_EMAIL, EMAIL_PASSWORD, TO_EMAIL");
    Console.WriteLine("Set them and re-run the application to send a test email.");
    return;
}

if (!int.TryParse(smtpPortStr, out var smtpPort))
{
    Console.WriteLine($"Invalid SMTP_PORT: '{smtpPortStr}'");
    return;
}

var logger = new EmailErrorLogger(smtpServer, smtpPort, fromEmail, password, toEmail);

var error = new ErrorDetails(
    "ERR-DEMO-001",
    "Demo Exception",
    "This is a demo error sent from DemoConsumerApp.",
    "https://example.com/demo"
);

try
{
    logger.LogError(error);
    Console.WriteLine("Send attempted. Check the recipient mailbox (and spam folder).");
}
catch (Exception ex)
{
    Console.WriteLine("Send failed: " + ex);
}
