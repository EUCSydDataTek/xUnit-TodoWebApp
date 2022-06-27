using TodoWebApp.Data;

namespace TodoWebApp.UnitTest.Utilities;
public static class SeedDatabase
{
    public static void SeedDatabaseTodoItems(this AppDbContext context)
    {
        context.TodoItems.AddRange(CreateTodoItems());
        
        context.SaveChanges();
    }

    

    private static TodoItem[] CreateTodoItems()
    {
        TodoItem[] items = {
            new TodoItem { Id = 1, TaskDescription = "Task 1", IsCompleted = false, Priority = PriorityLevel.Low },
            new TodoItem { Id = 2, TaskDescription = "Task 2", IsCompleted = true, Priority = PriorityLevel.Normal },
            new TodoItem { Id = 3, TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High, SubTasks = new List<SubTask>(){ new SubTask {Id = 1, SubTaskDescription = "En ny subtask"} } }
        };
        return items;
    }
}
