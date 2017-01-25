using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CrawlerWebApp.Validation
{
    public class FileContentTypeValidationAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            var contentType = file.ContentType;
            if (contentType.Equals("text/xml"))
                return true;
            else return false;
        }
    }
    public class FileExtensionAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            var extension = file.FileName.ToLowerInvariant().EndsWith("xsl");
            if (extension)
                return true;
            else return false;
        }
    }
    public class FileNameLenghAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            var fileNameLength = file.FileName.Length;
            if (fileNameLength > 255)
                return false;
            else return true;
        }
    }
}