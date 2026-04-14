using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorReportLibrary.Interface
{

    public interface IFileWriter
    {
        void Append(string path, string content);
    }

}
