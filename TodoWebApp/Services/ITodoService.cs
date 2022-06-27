using TodoWebApp.Models;

namespace TodoWebApp.Services;

public interface ITodoService
{
    List<TodoItem> GetAll();
    TodoItem GetItemById(Guid id);
    void Insert(TodoItem todoItem);
    void Update(TodoItem todoItem);
    void UpdateIsDone(TodoItem todoItem);
    void Remove(Guid id);
}
