using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AsyncEmail.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
        public IEnumerable<HttpPostedFileBase> Upload { get; set; }
    }
}