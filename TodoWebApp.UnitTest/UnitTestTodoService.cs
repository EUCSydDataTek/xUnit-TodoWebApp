using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Models;
using Xunit;

namespace TodoWebApp.UnitTest
{
    public class UnitTestTodoService
    {
        [Fact]
        public async Task Insert_TodoItems_Through_Service()
        {
            // ARRANGE: Create option object with InMemoryDatabase
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // ACT: Run the test against one instance of the service
            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                await service.Insert(new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low });
                await service.Insert(new TodoItem { TaskDescription = "Task 2", IsCompleted = false });
                await service.Insert(new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High });
            }

            // ASSERT: Use a separate instance of the service to verify correct data was saved to database
            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                var todoList = await service.GetAll();

                Assert.Equal(3, todoList.Count());

                TodoItem firstItem = await service.GetItemById(todoList[0].Id);
                Assert.Equal("Task 1", firstItem.TaskDescription);
                Assert.Equal("Low", firstItem.Priority.ToString());
                Assert.False(firstItem.IsCompleted);

                Assert.All(todoList, item => Assert.False(item.IsCompleted));
            }
        }

        [Fact]
        public async Task Check_TodoItem_Through_Service()
        {
            // ARRANGE: Create option object with InMemoryDatabase
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // ACT: Run the test against one instance of the service
            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                await service.Insert(new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low });
                await service.Insert(new TodoItem { TaskDescription = "Task 2", IsCompleted = false });
                await service.Insert(new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High });
            }

            // ASSERT: Use a separate instance of the service to verify correct data was saved to database
            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                var todoList = await service.GetAll();

                Assert.Equal(3, todoList.Count());

                TodoItem firstItem = await service.GetItemById(todoList[0].Id);
                Assert.Equal("Task 1", firstItem.TaskDescription);
                Assert.Equal("Low", firstItem.Priority.ToString());
                Assert.False(firstItem.IsCompleted);

                Assert.All(todoList, item => Assert.False(item.IsCompleted));
            }
        }
    }
}
