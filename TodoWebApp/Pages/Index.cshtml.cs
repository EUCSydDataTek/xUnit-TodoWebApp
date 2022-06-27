// https://www.learnrazorpages.com/razor-pages/forms/checkboxes
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Data;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITodoService _service;

    public IndexModel(ILogger<IndexModel> logger, ITodoService service)
    {
        _logger = logger;
        _service = service;
    }

    [BindProperty]
    public List<TodoItem> TodoItems { get; set; }

    [BindProperty]
    public PriorityLevel Priority { get; set; }


    public async Task OnGetCheckAsync(int id)
    {
        await _service.UpdateIsDone(id);
        TodoItems = await _service.GetAll();
    }

    public async Task OnGetAsync()
    {
        TodoItems = await _service.GetAll();
    }
}
