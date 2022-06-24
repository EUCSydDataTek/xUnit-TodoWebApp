using TodoWebApp.Data;

namespace TodoWebApp.Services;

public interface ITodoService
{
    Task<List<TodoItem>> GetAll();
    Task<List<TodoItem>> GetAllCompleted();
    Task<List<TodoItem>> GetAllNotCompleted();
    Task<TodoItem> GetItemById(Guid id);
    Task Insert(TodoItem todoItem);
    Task Update(TodoItem todoItem);
    Task UpdateIsDone(Guid id);
    Task Remove(Guid id);
}
