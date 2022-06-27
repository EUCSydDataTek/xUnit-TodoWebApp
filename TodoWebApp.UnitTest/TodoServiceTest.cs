using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using TodoWebApp.Data;
using TodoWebApp.Services;
using TodoWebApp.UnitTest.Utilities;
using Xunit.Extensions.AssertExtensions;

namespace TodoWebApp.UnitTest;

public class TodoServiceTest
{
    [Fact]
    public async Task Service_Insert()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();
        var service = new TodoService(context);

        context.ChangeTracker.Clear();  // Clear the context

        //ATTEMPT
        TodoItem newItem = new TodoItem { Id = 4, TaskDescription = "Task New", Priority = PriorityLevel.Low, SubTasks = new List<SubTask>() { new SubTask { Id = 2, SubTaskDescription = "En ny subtask" } } };
        await service.Insert(newItem);

        //VERIFY
        context.ChangeTracker.Clear();  // Clear the context
        context.TodoItems.Count().ShouldEqual(4);
        context.TodoItems
            .Include(t => t.SubTasks)
            .OrderBy(t => t.Id)
            .Last()
            .SubTasks.Count.ShouldEqual(1);

    }

    [Fact]
    public async Task Service_GetAll_and_GetAllNotCompleted_and_GetAllNotCompleted()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();
        var service = new TodoService(context);

        //ATTEMPT
        List<TodoItem> todoListAll = await service.GetAll();
        List<TodoItem> todoListCompleted = await service.GetAllCompleted();
        List<TodoItem> todoListNotCompleted = await service.GetAllNotCompleted();

        //VERIFY
        Assert.Equal(3, todoListAll.Count);
        Assert.Single(todoListCompleted);
        Assert.Equal(2, todoListNotCompleted.Count);
    }

    [Fact]
    public async Task Service_UpdateIsCompleted()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();
        var service = new TodoService(context);

        //ATTEMPT
        TodoItem todoItem = context.TodoItems
            .OrderBy(t => t.Id).Last();
        await service.UpdateIsDone(todoItem.Id);     // simulating an item is being checked

        //VERIFY
        context.ChangeTracker.Clear();              // Clear the context

        TodoItem updatedTodoItem = await service.GetItemById(todoItem.Id);
        updatedTodoItem.IsCompleted.ShouldBeTrue();

        var todoListNotCompleted = await service.GetAllNotCompleted();
        todoListNotCompleted.Count.ShouldEqual(1);

        Assert.All(todoListNotCompleted, item => Assert.False(item.IsCompleted));
    }

    [Fact]
    public async Task Service_Update()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();

        var service = new TodoService(context);
        TodoItem todoItem = await service.GetItemById(3);
        context.ChangeTracker.Clear();              // Clear the context

        //ATTEMPT
        todoItem.TaskDescription = "Task is updated";
        todoItem.Priority = PriorityLevel.Low;
        todoItem.IsCompleted = true;
        await service.Update(todoItem);

        // ASSERT
        TodoItem updatedTodoItem = await service.GetItemById(3);
        updatedTodoItem.TaskDescription.ShouldEqual("Task is updated");
        updatedTodoItem.Priority.ToString().ShouldEqual("Low");
        updatedTodoItem.IsCompleted.ShouldBeTrue();
    }

    [Fact]
    public async Task Service_Remove()
    {
        //SETUP
        var options = SqliteInMemory.CreateOptions<AppDbContext>();
        using var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseTodoItems();

        var service = new TodoService(context);
        TodoItem todoItem = await service.GetItemById(3);
        context.ChangeTracker.Clear();              // Clear the context

        //ATTEMPT
        await service.Remove(todoItem.Id);
        context.ChangeTracker.Clear();              // Clear the context

        // ASSERT
        todoItem = await service.GetItemById(3);
        todoItem.ShouldBeNull();
    }
}