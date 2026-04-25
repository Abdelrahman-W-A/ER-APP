using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses_Recorder_App.Models.Application_Models.Expenses
{
    public class Expense
    {
        public int Id { get; set; } // PK

        #region Expense Properties
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        #endregion

        #region RelationWithCategories
        public int CategoryId { get; set; } // FK to Category
        public Categories.Category Category { get; set; } = null!; // Navigation property 
        #endregion

        #region RelationWithUser
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        [InverseProperty(nameof(User.Expenses))]
        public User.User User { get; set; } = null!;
        #endregion
    }
}
