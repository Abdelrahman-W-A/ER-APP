using Expenses_Recorder_App.Models.Application_Models.Expenses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses_Recorder_App.Models.Application_Models.Categories
{
    public class Category
    {
        public int Id { get; set; } // PK

        #region Category Properties
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        #endregion
        
        #region RelationWithExpenses
        [InverseProperty(nameof(Expense.Category))]
        public IEnumerable<Expense>? Expenses { get; set; }
        #endregion

        [ForeignKey(nameof(Users))]
        public int UserID { get; set; }

        #region RelationWithUsers
        [InverseProperty(nameof(User.User.Categories))]
        public IEnumerable<User.User>? Users { get; set; }
        #endregion

    }
}
