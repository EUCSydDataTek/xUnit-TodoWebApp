using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository _repos;

        public DeleteModel(IRepository repos)
        {
            _repos = repos;
        }

        public TodoItem TodoItem { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TodoItem = await _repos.GetItemById(id);
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TodoItem TodoItem = await _repos.GetItemById(id);

            if (TodoItem != null)
            {
                _repos.Remove(TodoItem.Id);
                return RedirectToPage("./Index");
            }
            return NotFound();
        }
    }
}
