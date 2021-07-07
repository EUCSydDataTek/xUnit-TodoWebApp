using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebApp.Models;

namespace TodoWebApp.Data
{
    public class Repository : IRepository
    {
        List<TodoItem> TodoItemList;

        public Repository()
        {
            TodoItemList = new List<TodoItem>
            {
                new TodoItem { TaskDescription = "Task 1", Priority = PriorityLevel.Important},
                new TodoItem { TaskDescription = "Task 2", IsCompleted = false }
            };
        }
        public List<TodoItem> GetAll()
        {
            return TodoItemList;
            //return TodoItemList.Where(t => t.IsCompleted == false).OrderBy(t => t.CreatedTime).ToList();
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
}
