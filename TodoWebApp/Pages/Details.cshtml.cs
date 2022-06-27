using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Data;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class DetailsModel : PageModel
{
    private readonly ITodoService _service;

    public DetailsModel(ITodoService service)
    {
        _service = service;
    }

    public TodoItem TodoItem { get; set; }

    public async Task<IActionResult> OnGet(int id = 0)
    {
        if (id == 0)
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
}
