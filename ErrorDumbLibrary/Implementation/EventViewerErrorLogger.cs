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
            StringBuilder LogMessage = new StringBuilder();
            LogMessage.AppendLine($"Error Code: {error.ErrorCode}");
            LogMessage.AppendLine($"Title: {error.Title}");
            LogMessage.AppendLine($"Description: {error.Description}");
            LogMessage.AppendLine($"Help URL: {error.HelpUrl}");

            //Console.WriteLine(LogMessage.ToString());

            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }

            EventLog.WriteEntry(SourceName, LogMessage.ToString(), EventLogEntryType.Error);
        }
    }
}
