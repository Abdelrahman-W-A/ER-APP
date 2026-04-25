namespace Expenses_Recorder_App.Models.Application_Models.Categories
{
    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = null!;
        public int CategoriesCount => Categories.Count();
    }
}
