using System.ComponentModel.DataAnnotations;

namespace Expenses_Recorder_App.Models.Application_Models.User
{
    public class EnterEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

    }
}
