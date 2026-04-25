using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expenses_Recorder_App.Models.Application_Models.Expenses
{
    public class ExpenseCreateEditViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string? Note { get; set; }
        public IEnumerable<Categories.Category> Categories { get; set; } = new List<Categories.Category>();
    }
}
