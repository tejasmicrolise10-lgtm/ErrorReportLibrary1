using ErrorReportLibrary.Implementation;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;

namespace ErrorReportLibrary.Unit.Test;


public class EmailErrorLoggerTest
{
    private IErrorLogger _errorLogger;
    private ErrorDetails _errorDetails;
    private TestDoubles.FakeMailSender _fakeMailSender;
    private const string TestToEmail = "harshvardhan.microlise@gmail.com";

    [SetUp]
    public void Setup()
    {
        _fakeMailSender = new TestDoubles.FakeMailSender();
        _errorLogger = new EmailErrorLogger(TestToEmail, _fakeMailSender);

        _errorDetails = new ErrorDetails(
               "ERR001",
               "Index Out of Range Exception",
               "Index was outside the bounds of the array.",
               "https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception"
               );

    }

    [Test]
    public void LogError_EmailErrorLogger_ShouldMailAnError()
    {

        _errorLogger.LogError(_errorDetails);

        // Verify mail was composed and sent via the fake sender

        var sent = _fakeMailSender.SentMessage;
        Assert.That(sent, Is.Not.Null);
        Assert.That(sent.Subject, Does.Contain("Index Out of Range Exception"));
        Assert.That(sent.Body, Does.Contain("ERR001"));
        Assert.That(sent.To, Has.Some.Matches<System.Net.Mail.MailAddress>(m => m.Address == TestToEmail));
    }
}
