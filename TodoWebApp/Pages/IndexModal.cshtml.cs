// https://www.learnrazorpages.com/razor-pages/forms/checkboxes
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Data;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class IndexModalModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITodoService _service;

    public IndexModalModel(ILogger<IndexModel> logger, ITodoService service)
    {
        _logger = logger;
        _service = service;
    }

    [BindProperty]
    public List<TodoItem> TodoItems { get; set; }

    [BindProperty]
    public PriorityLevel Priority { get; set; }

    public async Task OnGet()
    {
        TodoItems = await _service.GetAllNotCompleted();
    }

    public async Task OnPost()
    {
        // Bemærk at denne måde at finde det selectede item på kræver at der ikke vises andre selectede items forinden.
        // Dvs. at man skal benytte GetAllNotCompleted() metoden og ikke GetAll() til at præsenterer items.
        TodoItem item = TodoItems.FirstOrDefault(t => t.IsCompleted == true);

        await _service.UpdateIsDone(item.Id);

        TodoItems = await _service.GetAllNotCompleted();
    }

    // Modal
    [BindProperty]
    public TodoItem TodoItem { get; set; }

    public IActionResult OnPostModalAdd()
    {
        if (ModelState.IsValid)
        {
            _service.Insert(TodoItem);
            return RedirectToPage("./IndexModal");
        }

        return Page();
    }
}

