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
        private string SourceName;
        private string LogName ="Application";

        public EventViewerErrorLogger(string SourceName)
        {
            this.SourceName = SourceName;
             
        }

        public void LogError(ErrorDetails error)
        {

            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }

            EventLog.WriteEntry(SourceName,error.FormatErrorMessage(), EventLogEntryType.Error);
        }
    }
}
