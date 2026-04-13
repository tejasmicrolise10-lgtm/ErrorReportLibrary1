using System.Text;
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
            StringBuilder LogMessage =new StringBuilder();

            LogMessage.AppendLine($"Error Code{error.ErrorCode}");
            LogMessage.AppendLine($"Title{error.Title}");
            LogMessage.AppendLine($"Description{error.Description}");
            LogMessage.AppendLine($"Help URL{error.HelpUrl}");
           

            File.AppendAllText(logFilePath,LogMessage.ToString());
        }
    }
}
