using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Models;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITodoService _context;

    public IndexModel(ILogger<IndexModel> logger, ITodoService context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public List<TodoItem> TodoItems { get; set; }

    [BindProperty]
    public PriorityLevel Priority { get; set; }

    public Guid MyGuid { get; set; } = Guid.NewGuid();

    public void OnGet()
    {
        TodoItems = _context.GetAll();
    }

    public void OnPost()
    {
        foreach (TodoItem item in TodoItems)
        {
            if (item.IsCompleted)
            {
                _context.UpdateIsDone(item);
            }
        }
        TodoItems = _context.GetAll();
    }
}
