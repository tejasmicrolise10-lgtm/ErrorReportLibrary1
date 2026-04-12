using ErrorReportLibrary.Implementation;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using NUnit.Framework;
using System.Globalization;
using System.IO;

namespace ErrorReportLibrary.Unit.Test
{
    public class Tests
    {
        private IErrorLogger _errorLogger;
        private string _logFilePath;
        private string tempPath;
        private const string FileName = "ErrorLogs.txt";
        private ErrorDetails errorDetails;

       [SetUp]
        public void Setup()
        {
            _errorLogger = new FileErrorLogger(FileName);
            tempPath = Path.GetTempPath();
            _logFilePath = Path.Combine(tempPath, FileName);
            errorDetails = new ErrorDetails(
                "ERR001",
                "Index Out of Range Exception",
                "Index was outside the bounds of the array.",
                "https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception"
                );
        }

        [Test]
        public void LogError_FileErrorLogger_CreatesALogFile()
        {         
            _errorLogger.LogError(errorDetails);
            Assert.That(File.Exists(_logFilePath), Is.True);
        }

        [Test]
        public void LogError_FileErrorLogger_withValidError_LogsCorrectErrorDetailsInFile()
        {
            _errorLogger.LogError(errorDetails);
            string logContent = File.ReadAllText(_logFilePath);
            Assert.That(logContent, Does.Contain("ERR001"));
            Assert.That(logContent, Does.Contain("Index Out of Range Exception"));
            Assert.That(logContent, Does.Contain("Index was outside the bounds of the array."));
            Assert.That(logContent, Does.Contain("https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception"));
        }

       
    }
}
    