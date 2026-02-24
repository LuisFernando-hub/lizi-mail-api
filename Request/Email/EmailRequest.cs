using System.ComponentModel.DataAnnotations;

namespace lizi_mail_api.Request.Email
{
    public class EmailRequest
    {
        [Required(ErrorMessage = "The 'to' field is required.")]
        [EmailAddress(ErrorMessage = "The 'to' field must be a valid email address.")]
        public string to { get; set; }
        [Required(ErrorMessage = "The 'subject' field is required.")]
        public string subject { get; set; }
        [Required(ErrorMessage = "The 'body' field is required.")]
        public string body { get; set; }
    }
}
