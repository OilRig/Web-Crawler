using CrawlerWebApp.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CrawlerWebApp.Models
{
    public class CrawlerValidationModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Range(typeof(int), "0", "10", ErrorMessage = "Must be more then 0, but less or equal to 10 to avoid DOS-attack")]
        public int ThemeLimitator { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(typeof(int), "0", "10", ErrorMessage = "Must be more then 0, but less equal to 10 to avoid DOS-attack")]
        public int ThreadLimitator { get; set; }

        [Required(ErrorMessage = "File is not attached")]
        [FileContentTypeValidation(ErrorMessage = "File type must be of XML")]
        [FileExtension(ErrorMessage = "File extension must be .xsl")]
        [FileNameLengh(ErrorMessage = "File name should be no longer then 255 characters")]
        //[FileExtensions(Extensions = "xsl", ErrorMessage = "File must be of xsl type")]
        public HttpPostedFileBase XslFile { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(30, MinimumLength = 5)]
        public string BoardName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string BoardUrl { get; set; }
    }
}