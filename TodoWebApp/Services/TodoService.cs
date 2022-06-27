using Microsoft.EntityFrameworkCore;
using TodoWebApp.Data;

namespace TodoWebApp.Services;

public class TodoService : ITodoService
{
    private readonly AppDbContext _context;

    public TodoService(AppDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public async Task<List<TodoItem>> GetAll()
    {
        return await _context.TodoItems.AsNoTracking().ToListAsync();
    }

    public async Task<List<TodoItem>> GetAllCompleted()
    {
        return await _context.TodoItems.Where(t => t.IsCompleted == true).OrderBy(t => t.CreatedTime).AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<TodoItem>> GetAllNotCompleted()
    {
        return await _context.TodoItems.Where(t => t.IsCompleted == false).OrderBy(t => t.CreatedTime).AsNoTracking().ToListAsync();
    }

    public async Task<TodoItem> GetItemById(int id)
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

    public async Task Remove(int id)
    {
        //TodoItem item = _context.TodoItems.Include(t => t.SubTasks).Where(t => t.Id == id).FirstOrDefault();
        //_context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateIsDone(int id)
    {
        var changedItem = _context.TodoItems.FirstOrDefault(t => t.Id == id);
        changedItem.IsCompleted = !changedItem.IsCompleted;
        await _context.SaveChangesAsync();
    }
}
