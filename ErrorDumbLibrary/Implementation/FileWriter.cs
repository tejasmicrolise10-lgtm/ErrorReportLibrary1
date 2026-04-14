using ErrorReportLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorReportLibrary.Implementation
{

    public class FileWriter : IFileWriter
    {
        public void Append(string path, string content)
        {
            File.AppendAllText(path, content);
        }
    }

}
