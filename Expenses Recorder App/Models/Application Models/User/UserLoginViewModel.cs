using System.ComponentModel.DataAnnotations;

namespace Expenses_Recorder_App.Models.Application_Models.User
{
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
