using TodoWebApp.Models;

namespace TodoWebApp.Services;

public class TodoService : ITodoService
{
    List<TodoItem> TodoItemList;

    public TodoService()
    {
        TodoItemList = new List<TodoItem>
        {
            new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Low},
            new TodoItem { TaskDescription = "Task 2", IsCompleted = false },
            new TodoItem { TaskDescription = "Task 3", IsCompleted = false, Priority = PriorityLevel.High }
        };
    }
    public List<TodoItem> GetAll()
    {
        //return TodoItemList;
        return TodoItemList.Where(t => t.IsCompleted == false).OrderBy(t => t.CreatedTime).ToList();
    }

    public TodoItem GetItemById(Guid id)
    {
        return TodoItemList.SingleOrDefault(t => t.Id == id);
    }

    public void Insert(TodoItem todoItem)
    {
        TodoItemList.Add(todoItem);
    }

    public void Update(TodoItem todoItem)
    {
        var item = TodoItemList.Find(t => t.Id == todoItem.Id);
        item.TaskDescription = todoItem.TaskDescription;
        item.Priority = todoItem.Priority;
        item.IsCompleted = todoItem.IsCompleted;
        item.CreatedTime = todoItem.CreatedTime;
    }

    public void Remove(Guid id)
    {
        TodoItemList.Remove(GetItemById(id));
    }

    public void UpdateIsDone(TodoItem todoItem)
    {
        var item = TodoItemList.Find(t => t.Id == todoItem.Id);
        item.IsCompleted = todoItem.IsCompleted;
    }
}
