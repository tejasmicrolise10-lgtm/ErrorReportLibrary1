using System;
using System.Collections.Generic;
using System.Text;
using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using System.Diagnostics;
 
namespace ErrorReportLibrary.Implementation
{
    public class EventViewerErrorLogger : IErrorLogger
    {
        private readonly IEventLogWrite _eventLogWriter;
        private readonly string _sourceName;
        private readonly string _logName = "Application";

        public EventViewerErrorLogger(IEventLogWrite eventLogWriter, string sourceName)
        {
            _eventLogWriter = eventLogWriter;
            _sourceName = sourceName;
        }

        public void LogError(ErrorDetails error)
        { 
             

            if (!_eventLogWriter.SourceExists(_sourceName))
            {
                _eventLogWriter.CreateSource(_sourceName, _logName);
            }

            _eventLogWriter.Write(_sourceName, error.FormatErrorMessage(), EventLogEntryType.Error);
        }
    }
}
