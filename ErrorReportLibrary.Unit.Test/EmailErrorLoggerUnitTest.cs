using ErrorReportLibrary.Implementation;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using Moq;
using System.Net.Mail;

namespace ErrorReportLibrary.Unit.Test;

public class NUnitTestItem1
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LogError_WhenCalled_SendsEmailWithCorrectContent()
    {
        // Arrange
        var mailSenderMock = new Mock<IMailSender>();

        var logger = new EmailErrorLogger(
            "from@test.com",
            "to@test.com",
            mailSenderMock.Object
        );

        var error = new ErrorDetails(
            "ERR001",
            "Index Out of Range",
            "Index was outside array bounds",
            "https://docs.microsoft.com"
        );

        // Act
        logger.LogError(error);

        // Assert
        mailSenderMock.Verify(
            m => m.Send(It.Is<MailMessage>(mail =>
                mail.Subject.Contains("Index Out of Range") &&
                mail.Body.Contains("ERR001") &&
                mail.Body.Contains("Index was outside array bounds")
            )),
            Times.Once
        );
    }
}
