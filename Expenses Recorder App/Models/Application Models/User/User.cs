using Expenses_Recorder_App.Models.Application_Models.Categories;
using Expenses_Recorder_App.Models.Application_Models.Expenses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses_Recorder_App.Models.Application_Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string UserQuestion { get; set; } = null!;
        public string UserQuestionAnswer { get; set; } = null!;

        [Required]
        public decimal Salary { get; set; }

        #region RelationWithExpenses
        [InverseProperty(nameof(Expense.User))]
        public IEnumerable<Expense>? Expenses { get; set; }
        #endregion
        
        #region RelationWithCategories
        [InverseProperty(nameof(Category.Users))]
        public IEnumerable<Category>? Categories { get; set; }
        #endregion

    }
}
