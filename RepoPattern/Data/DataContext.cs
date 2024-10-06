namespace Repository_Pattern.RepoPattern;

using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; }
}
