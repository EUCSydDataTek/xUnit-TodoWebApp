using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApp.Data;

namespace TodoWebApp.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAll();
        Task<List<TodoItem>> GetAllCompleted();
        Task<List<TodoItem>> GetAllNotCompleted();
        Task<TodoItem> GetItemById(Guid id);
        Task Insert(TodoItem todoItem);
        Task Update(TodoItem todoItem);
        Task UpdateIsDone(TodoItem todoItem);
        Task Remove(Guid id);
    }
}
