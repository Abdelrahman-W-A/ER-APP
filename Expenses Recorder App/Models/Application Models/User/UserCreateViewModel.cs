using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Expenses_Recorder_App.Models.Application_Models.User
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage ="Question is required For password reseting.")]
        public string UserQuestion { get; set; } = null!;

        [Required(ErrorMessage = "Answer is required for password reseting.")]
        public string UserAnswer { get; set; } = null!;

        [Required(ErrorMessage = "Salary is required.")]
        public decimal Salary { get; set; }
    }
}
