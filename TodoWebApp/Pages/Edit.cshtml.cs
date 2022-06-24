using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Data;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class EditModel : PageModel
{
    private readonly ITodoService _service;

    public EditModel(ITodoService service)
    {
        _service = service;
    }

    [BindProperty]
    public TodoItem TodoItem { get; set; }

    public async Task<IActionResult> OnGet(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        TodoItem = await _service.GetItemById(id);

        if (TodoItem == null)
        {
            return NotFound();
        }
        return Page();
    }

    public IActionResult OnPost(TodoItem TodoItem)
    {
        _service.Update(TodoItem);

        return RedirectToPage("./Index");
    }

    private async Task<bool> PersonExists(Guid id)
    {
        TodoItem p = await _service.GetItemById(id);

        return p != null;
    }
}
