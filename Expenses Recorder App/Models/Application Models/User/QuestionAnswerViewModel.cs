using System.ComponentModel.DataAnnotations;

namespace Expenses_Recorder_App.Models.Application_Models.User
{
    public class QuestionAnswerViewModel
    {

        public string UserQuestion { get; set; } = null!;

        [Required (ErrorMessage = "Answer is required.")]
        public string UserQuestionAnswer { get; set; } = null!;

    }
}
