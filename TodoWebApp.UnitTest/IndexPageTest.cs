using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Pages;
using TodoWebApp.Services;
using TodoWebApp.UnitTest.Utilities;
using Xunit;
using System.Linq;

namespace TodoWebApp.UnitTest
{
    public class IndexPageTest
    {
        [Fact]
        public async Task OnGetAsync_PopulatesThePageModel_WithAListOfMessages()
        {
            using (var context = new AppDbContext(Utility.TestDbContextOptions()))
            {
                // ARRANGE
                var service = new TodoService(context);
                var expectedTodoItems = await service.GetAllNotCompleted();

                var mockTodoService = new Mock<TodoService>(context);

                // Husk at GetAllNotCompleted() metoden skal være virtual!
                mockTodoService.Setup(s => s.GetAllNotCompleted()).Returns(Task.FromResult(expectedTodoItems));

                var pageModel = new IndexModel(null, mockTodoService.Object);

                // ACT
                await pageModel.OnGetAsync();

                // ASSERT
                var actualTodoList = Assert.IsAssignableFrom<List<TodoItem>>(pageModel.TodoItems);
                Assert.Equal(expectedTodoItems.Select(t => t.Id), actualTodoList.Select(t => t.Id));
            }
        }
    }
}
