using System;
using System.Collections.Generic;
using TodoWebApp.Models;

namespace TodoWebApp.Data
{
    public interface IRepository
    {
        List<TodoItem> GetAll();
        TodoItem GetItemById(Guid id);
        void Insert(TodoItem todoItem);
        void Update(TodoItem todoItem);
        void UpdateIsDone(TodoItem todoItem);
        void Remove(Guid id);
    }
}
