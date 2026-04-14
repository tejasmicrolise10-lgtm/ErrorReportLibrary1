using ErrorReportLibrary.Interface;
using ErrorReportLibrary.Model;

namespace ErrorReportLibrary.Implementation
{   

     public class FileErrorLogger : IErrorLogger
    {
        private string tempPath;
        private string logFilePath;
    
        public FileErrorLogger(string FileName)
        {

            tempPath = Path.GetTempPath();    
            logFilePath = Path.Combine(tempPath, FileName);

        }

     
        public void LogError(ErrorDetails error)
        {
            File.AppendAllText(logFilePath, error.FormatErrorMessage());
        }
    }
}
