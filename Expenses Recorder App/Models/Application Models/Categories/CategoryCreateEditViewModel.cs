namespace Expenses_Recorder_App.Models.Application_Models.Categories
{
    public class CategoryCreateEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
    }
}
