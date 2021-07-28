using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApp.Models;

namespace TodoWebApp.Data
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAll();
        Task<TodoItem> GetItemById(Guid id);
        Task Insert(TodoItem todoItem);
        Task Update(TodoItem todoItem);
        Task UpdateIsDone(List<TodoItem> todoItems);
        Task Remove(Guid id);
    }
}
