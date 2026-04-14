using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;
using System.IO;
namespace ErrorReportLibrary.Implementation
{   

     public class FileErrorLogger : IErrorLogger
    {
        private string tempPath;
        private IFileWriter _fileWriter ;
        private string logFilePath;
    
        public FileErrorLogger(IFileWriter fileWriter,string FileName)
        {

            tempPath = Path.GetTempPath();    
            logFilePath = Path.Combine(tempPath, FileName);
            this._fileWriter = fileWriter;

        }

     
        public void LogError(ErrorDetails error)
        {
             


            _fileWriter.Append(logFilePath, error.FormatErrorMessage() );
        }
    }
}
