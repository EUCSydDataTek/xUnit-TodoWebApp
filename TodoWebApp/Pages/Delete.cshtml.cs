using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TodoWebApp.Services;
using TodoWebApp.Data;

namespace TodoWebApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ITodoService _service;

        public DeleteModel(ITodoService service)
        {
            _service = service;
        }

        public TodoItem TodoItem { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TodoItem = await _service.GetItemById(id);
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TodoItem TodoItem = await _service.GetItemById(id);

            if (TodoItem != null)
            {
                await _service.Remove(TodoItem.Id);
                return RedirectToPage("./Index");
            }
            return NotFound();
        }
    }
}
