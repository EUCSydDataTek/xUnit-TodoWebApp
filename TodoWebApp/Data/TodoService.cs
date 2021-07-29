using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApp.Models;

namespace TodoWebApp.Data
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
            //_context.Database.EnsureCreated();
        }

        public async Task<List<TodoItem>> GetAll()
        {
            //return await _context.TodoItems;
            return await _context.TodoItems.Where(t => t.IsCompleted == false).OrderBy(t => t.CreatedTime).ToListAsync();
        }

        public async Task<TodoItem> GetItemById(Guid id)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Insert(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoItem todoItem)
        {
            _context.Attach(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            TodoItem item = _context.TodoItems.Find(id);
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIsDone(List<TodoItem> todoItems)
        {
            foreach (TodoItem item in todoItems)
            {
                if (item.IsCompleted)
                {
                    var changedItem = _context.TodoItems.FirstOrDefault(t => t.Id == item.Id);
                    changedItem.IsCompleted = item.IsCompleted;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
