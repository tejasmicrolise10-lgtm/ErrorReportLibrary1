using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ErrorReportLibrary.Model
{
    public class ErrorDetails
    {
        public string? ErrorCode { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? HelpUrl { get; set; }

        public ErrorDetails(String ErrorCode, string Title, String Description, string HelpUrl)
        {
            this.ErrorCode = ErrorCode;
            this.Title = Title;
            this.Description = Description;
            this.HelpUrl = HelpUrl;
        }

        public string FormatErrorMessage()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Error Code: {ErrorCode}");
            message.AppendLine($"Title: {Title}");
            message.AppendLine($"Description: {Description}");
            message.AppendLine($"Help URL: {HelpUrl}");
            return message.ToString();
        }
    }
}
