using ErrorReportLibrary.Implementation;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using Moq;
using System.Timers;

namespace ErrorReportLibrary.Unit.Test;

public class FileErrorLoggerUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LogError_WhenCalled_WritesFormattedErrorToFile()
    {
        // Arrange
        var fileWriterMock = new Mock<IFileWriter>();
        var logger = new FileErrorLogger(fileWriterMock.Object, "ErrorLogs.txt");

        var error = new ErrorDetails(
            "ERR001",
            "Index Out of Range",
            "Index was outside array bounds",
            "https://docs.microsoft.com"
        );

        // Act
        logger.LogError(error);

        // Assert
        fileWriterMock.Verify(
            fw => fw.Append(
                It.Is<string>(path => path.Contains("ErrorLogs.txt")),
                It.Is<string>(content =>
                    content.Contains("ERR001") &&
                    content.Contains("Index Out of Range") &&
                    content.Contains("Index was outside array bounds") &&
                    content.Contains("https://docs.microsoft.com")
                )
            ),
            Times.Once
        );
    }
}
