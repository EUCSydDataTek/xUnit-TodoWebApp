using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using TodoWebApp.Data;
using TodoWebApp.UnitTest.Utilities;
using Xunit.Extensions.AssertExtensions;

namespace TodoWebApp.UnitTest;
public class TodoEFcoreTest
{
    [Fact]
    public void Number_of_TodoItems_in_Database()
    {
        //SETUP (Arrange)
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();

        //ATTEMPT (Act)
        var query = context.TodoItems;
        var todoItems = query.ToList();

        //VERIFY (Assert)
        Assert.Equal(3, todoItems.Count);
        //todoItems.Count.ShouldEqual(3);

    }

    [Fact]
    public void Number_of_Completed_TodoItems_in_Database()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();

        //ATTEMPT
        var query = context.TodoItems.Where(t => t.IsCompleted);
        var todoItems = query.ToList();

        //VERIFY
        todoItems.Count.ShouldEqual(1);
    }

    //Denne test er ikke korrekt fordi den ikke tester om SubTask collection bliver null i en Disconnected situation (i TodoItem.SubTasks fjernes: = new List<SubTask>(); 
    [Fact]
    public void Incorrect_simulation_of_Disconnected_Data_in_Database()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();

        //ATTEMPT
        TodoItem todoItem = context.TodoItems
           .OrderBy(t => t.Id).Last();
        todoItem.SubTasks.Add(new SubTask { SubTaskDescription = "En SubTask forsøges indsat" });
        context.SaveChanges();

        //VERIFY
        context.TodoItems
             .Include(t => t.SubTasks)
             .OrderBy(t => t.Id)
             .Last()
             .SubTasks.Count.ShouldEqual(2);
    }

    //Denne test fejler hvis property SubTasks ikke sættes lig et List<SubTask> objekt når SubTask-objektet oprettes
    // eller man alternativt benytter Include(t => t.SubTasks) i query.
    //Fejlen viser sig kun hvis man clearer contexten.
    [Fact]
    public void Correct_simulation_of_Disconnected_Data_in_Database()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();

        context.ChangeTracker.Clear();  // Clear the context to simulate a new context

        //ATTEMPT
        TodoItem todoItem = context.TodoItems
            .Include(t => t.SubTasks)
            .OrderBy(t => t.Id).Last();
        todoItem.SubTasks.Add(new SubTask { SubTaskDescription = "En SubTask forsøges indsat" });
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();  // Clear the context

        context.TodoItems
            .Include(t => t.SubTasks)
            .OrderBy(t => t.Id)
            .Last()
            .SubTasks.Count.ShouldEqual(2);
    }

    [Fact]
    public void Deleting_a_TodoItem_in_Database_With_Relating_SubTask()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();
        context.ChangeTracker.Clear();  // Clear the context

        //ATTEMPT
        TodoItem todoItem = context.TodoItems
            .Include(t => t.SubTasks)
            .OrderBy(t => t.Id).Last();
        context.TodoItems.Remove(todoItem);
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();  // Clear the context
        context.TodoItems
            .Count().ShouldEqual(2);
    }
}
