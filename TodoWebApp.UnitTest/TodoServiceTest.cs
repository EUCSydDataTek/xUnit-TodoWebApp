using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.UnitTest.Utilities;
using Xunit;

namespace TodoWebApp.UnitTest
{
    public class TodoServiceTest
    {
        [Fact]
        public async Task Service_Insert_and_GetItemById()
        {
            using (var context = new AppDbContext(Utility.TestDbContextOptions()))
            {
                // ARRANGE
                var service = new Services.TodoService(context);

                // ACT
                TodoItem firstItem = new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low };
                await service.Insert(firstItem);

                // ASSERT
                TodoItem item = await service.GetItemById(firstItem.Id);

                item = await service.GetItemById(firstItem.Id);
                Assert.Equal("Task 1", item.TaskDescription);
                Assert.Equal("Low", item.Priority.ToString());
                Assert.False(item.IsCompleted);
            }
        }

        [Fact]
        public async Task Service_GetAll_and_GetAllNotCompleted_and_GetAllNotCompleted()
        {
            using (var context = new AppDbContext(Utility.TestDbContextOptions()))
            {
                // ARRANGE
                var service = new Services.TodoService(context);
                await service.Insert(new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low });
                await service.Insert(new TodoItem { TaskDescription = "Task 2", IsCompleted = false });
                await service.Insert(new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High });
                await service.Insert(new TodoItem { TaskDescription = "Task 4", IsCompleted = true, Priority = PriorityLevel.High });

                // ACT
                // ASSERT
                List<TodoItem> todoList = await service.GetAll();
                Assert.Equal(4, todoList.Count);

                todoList = await service.GetAllNotCompleted();
                Assert.Equal(3, todoList.Count);

                todoList = await service.GetAllCompleted();
                Assert.Single(todoList);
            }
        }

        [Fact]
        public async Task Service_UpdateIsDone()
        {
            using (var context = new AppDbContext(Utility.TestDbContextOptions()))
            {
                // ARRANGE
                var service = new Services.TodoService(context);
                await service.Insert(new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low });
                await service.Insert(new TodoItem { TaskDescription = "Task 2", IsCompleted = false });
                TodoItem itemThird = new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High };
                await service.Insert(itemThird);

                // ACT
                await service.UpdateIsDone(itemThird.Id);     // simulating an item is being checked

                // ASSERT
                var todoList = await service.GetAllNotCompleted();
                Assert.Equal(2, todoList.Count());
                Assert.All(todoList, item => Assert.False(item.IsCompleted));
            }
        }

        [Fact]
        public async Task Service_Update()
        {
            using (var context = new AppDbContext(Utility.TestDbContextOptions()))
            {
                // ARRANGE
                var service = new Services.TodoService(context);
                await service.Insert(new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low });
                await service.Insert(new TodoItem { TaskDescription = "Task 2", IsCompleted = false });
                TodoItem itemThird = new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High };
                await service.Insert(itemThird);

                // ACT
                TodoItem item = await service.GetItemById(itemThird.Id);
                item.TaskDescription = "Task is updated";
                item.Priority = PriorityLevel.Low;
                item.IsCompleted = true;

                await service.Update(item);

                // ASSERT
                item = await service.GetItemById(itemThird.Id);

                Assert.Equal("Task is updated", item.TaskDescription);
                Assert.Equal("Low", item.Priority.ToString());
                Assert.True(item.IsCompleted);
            }
        }

        [Fact]
        public async Task Service_Remove()
        {
            using (var context = new AppDbContext(Utility.TestDbContextOptions()))
            {
                // ARRANGE
                var service = new Services.TodoService(context);
                await service.Insert(new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low });
                await service.Insert(new TodoItem { TaskDescription = "Task 2", IsCompleted = false });
                TodoItem itemThird = new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High };
                await service.Insert(itemThird);

                // ACT
                TodoItem item = await service.GetItemById(itemThird.Id);
                await service.Remove(item.Id);

                // ASSERT
                item = await service.GetItemById(itemThird.Id);
                Assert.Null(item);
            }
        }
    }
}
