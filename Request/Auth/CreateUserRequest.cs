using System.ComponentModel.DataAnnotations;

namespace lizi_mail_api.Request.Auth
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3)]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3)]
        public string password { get; set; }
    }
}
