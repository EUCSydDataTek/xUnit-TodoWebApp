using Microsoft.EntityFrameworkCore;

namespace TodoWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low },
                new TodoItem { TaskDescription = "Task 2", IsCompleted = false },
                new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High }
                );
        }
    }
}
