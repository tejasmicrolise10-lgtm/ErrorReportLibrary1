using ErrorReportLibrary.Implementation;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
namespace ErrorReportLibrary.Unit.Test;

public class EventViewerErrorLoggerTest
{
    private IErrorLogger _errorLogger;
    private ErrorDetails _errorDetails;
    private EventLog _eventLog;
    private const string TestSourceName = "LMS";
    [SetUp]
    public void Setup()
    {
        _errorLogger = new EventViewerErrorLogger(TestSourceName);
        _errorDetails = new ErrorDetails(
                "ERR001",
                "Index Out of Range Exception",
                "Index was outside the bounds of the array.",
                "https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception"
                );
        // The event source 'LMS' is registered under the Application log; open Application log
        _eventLog = new EventLog("Application");

    }

    [Test]
    public void LogError_EventViewerErrorLogger_LogsErrorToEventViewer()
    {
        _errorLogger.LogError(_errorDetails);
        // Find the most recent entry written by our test source
        var entry = _eventLog.Entries.Cast<EventLogEntry>().LastOrDefault(e => e.Source == TestSourceName);

        Assert.That(entry, Is.Not.Null, $"No event log entry with source '{TestSourceName}' was found.");
        Assert.That(entry.EntryType, Is.EqualTo(EventLogEntryType.Error));
        Assert.That(entry.Source, Is.EqualTo(TestSourceName));
        Assert.That(entry.Message, Does.Contain("ERR001"));
        Assert.That(entry.Message,Does.Contain("Index Out of Range Exception"));
        Assert.That(entry.Message, Does.Contain("Index was outside the bounds of the array."));
        Assert.That(entry.Message, Does.Contain("https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception"));
    }

    [TearDown]
    public void TearDown()
    {

        // Dispose the EventLog instance
        _eventLog?.Dispose();

        // Remove created event source if present (requires permissions)
        if (EventLog.SourceExists(TestSourceName))
        {
            EventLog.DeleteEventSource(TestSourceName);
        }
    }
}
