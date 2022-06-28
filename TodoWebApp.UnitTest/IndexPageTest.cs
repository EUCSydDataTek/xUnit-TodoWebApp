//using TestSupport.EfHelpers;
//using TodoWebApp.Data;
//using TodoWebApp.Pages;
//using TodoWebApp.Services;

//// Der er et problem med Moq og TestSupport.EfHelpers, som ikke begge kan være installeret.

//namespace TodoWebApp.UnitTest;
//public class IndexPageTest
//{
//    [Fact]
//    public async Task OnGetAsync_PopulatesThePageModel_WithAListOfMessages()
//    {
//        // ARRANGE
//        var options = SqliteInMemory.CreateOptions<AppDbContext>();
//        using var context = new AppDbContext(options);

//        var service = new TodoService(context);
//        var expectedTodoItems = await service.GetAllNotCompleted();

//        var mockTodoService = new Mock<TodoService>(context);

//        // Husk at GetAllNotCompleted() metoden skal være virtual!
//        mockTodoService.Setup(s => s.GetAllNotCompleted()).Returns(Task.FromResult(expectedTodoItems));

//        var pageModel = new IndexModel(null, mockTodoService.Object);

//        // ACT
//        await pageModel.OnGetAsync();

//        // ASSERT
//        var actualTodoList = Assert.IsAssignableFrom<List<TodoItem>>(pageModel.TodoItems);
//        Assert.Equal(expectedTodoItems.Select(t => t.Id), actualTodoList.Select(t => t.Id));
//    }
//}
