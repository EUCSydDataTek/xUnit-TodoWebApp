using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Models;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class DetailsModel : PageModel
{
    private readonly ITodoService _context;

    public DetailsModel(ITodoService context)
    {
        _context = context;
    }

    public TodoItem TodoItem { get; set; }

    public IActionResult OnGet(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        TodoItem = _context.GetItemById(id);

        if (TodoItem == null)
        {
            return NotFound();
        }
        return Page();
    }
}
