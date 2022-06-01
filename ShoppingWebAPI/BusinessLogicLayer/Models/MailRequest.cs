using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class MailRequest
    {
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
