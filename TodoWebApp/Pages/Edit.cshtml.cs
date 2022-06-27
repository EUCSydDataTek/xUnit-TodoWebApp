using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Models;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class EditModel : PageModel
{
    private readonly ITodoService _context;

    public EditModel(ITodoService context)
    {
        _context = context;
    }

    [BindProperty]
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

    public IActionResult OnPost(TodoItem TodoItem)
    {
        _context.Update(TodoItem);

        return RedirectToPage("./Index");
    }

    private bool PersonExists(Guid id)
    {
        TodoItem p = _context.GetItemById(id);

        return p != null;
    }
}
