using ErrorReportLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ErrorReportLibrary.Implementation
{

    public class WindowsEventLogWriter : IEventLogWrite
    {
        public bool SourceExists(string source)
            => EventLog.SourceExists(source);

        public void CreateSource(string source, string logName)
            => EventLog.CreateEventSource(source, logName);

        public void Write(string source, string message, EventLogEntryType type)
            => EventLog.WriteEntry(source, message, type);
    }

}
