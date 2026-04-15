using ErrorReportLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorReportLibrary.Interface
{
    public interface IErrorLogger
    {
        void LogError(ErrorDetails error);
    }
}
