using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ErrorReportLibrary.Interface
{
    public interface IEventLogWrite
    {

        bool SourceExists(string source);
        void CreateSource(string source, string logName);
        void Write(string source, string message, EventLogEntryType type);

    }
}
