using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Models;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class DeleteModel : PageModel
{
    private readonly ITodoService _context;

    public DeleteModel(ITodoService context)
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
        return Page();
    }

    public IActionResult OnGetDelete(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        TodoItem TodoItem = _context.GetItemById(id);

        if (TodoItem != null)
        {
            _context.Remove(TodoItem.Id);
            return RedirectToPage("./Index");
        }
        return NotFound();
    }
}
