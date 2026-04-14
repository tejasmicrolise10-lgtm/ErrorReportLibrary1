using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using Moq;
using ErrorReportLibrary.Implementation;
using System.Diagnostics;
namespace ErrorReportLibrary.Unit.Test;

public class EventViewerErrorLoggerTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LogError_WhenSourceDoesNotExist_CreatesSourceAndWritesEvent()
    {
        var mockWriter = new Mock<IEventLogWrite>();

        mockWriter.Setup(w => w.SourceExists("MyApp"))
                  .Returns(false);

        var logger = new EventViewerErrorLogger(mockWriter.Object, "MyApp");

        var error = new ErrorDetails(
            "ERR001",
            "Index Out of Range",
            "Index was outside array bounds",
            "https://docs.microsoft.com"
        );

        logger.LogError(error);

        mockWriter.Verify(w => w.CreateSource("MyApp", "Application"), Times.Once);
        mockWriter.Verify(w => w.Write(
            "MyApp",
            It.Is<string>(msg => msg.Contains("ERR001")),
            EventLogEntryType.Error),
            Times.Once);
    }
}
