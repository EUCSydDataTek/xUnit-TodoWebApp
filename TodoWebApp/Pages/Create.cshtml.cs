using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Data;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public class CreateModel : PageModel
{
    private readonly ITodoService _service;

    public CreateModel(ITodoService service)
    {
        _service = service;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public TodoItem TodoItem { get; set; }


    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            await _service.Insert(TodoItem);
            return RedirectToPage("./Index");
        }

        return Page();
    }
}
