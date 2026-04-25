using Expenses_Recorder_App.Models.Application_Models.Categories;

namespace Expenses_Recorder_App.Models.Application_Models.Expenses
{
    public class ExpenseViewModel
    {
        public decimal MonthSalary { get; set; }
        public decimal RemainingSalary { get { return MonthSalary - MonthlyTotal; } }
        public List<Category> Category { get; set; } = new();
        public decimal MonthlyTotal { get; set; }
        public decimal YearlyTotal { get; set; }
        public int CategoriesCount { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<Expense> Expenses { get; set; } = new();
    }
}
