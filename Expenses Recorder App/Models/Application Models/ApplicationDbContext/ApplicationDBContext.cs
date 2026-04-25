using Microsoft.EntityFrameworkCore;

namespace Expenses_Recorder_App.Models.Application_Models.ApplicationDbContext
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Expenses.Expense> Expenses { get; set; } // Table for Expenses
        public DbSet<Categories.Category> Categories { get; set; } // Table for Categories
        public DbSet<User.User> Users { get; set; } // Table for Users

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) // Constructor to initialize the DbContext with options
        {

        }
    }
}
