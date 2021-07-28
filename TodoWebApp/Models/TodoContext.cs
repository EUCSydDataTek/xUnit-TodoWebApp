using Microsoft.EntityFrameworkCore;

namespace TodoWebApp.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
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
