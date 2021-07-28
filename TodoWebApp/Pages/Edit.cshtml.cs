using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repos;

        public EditModel(IRepository repos)
        {
            _repos = repos;
        }

        [BindProperty]
        public TodoItem TodoItem { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TodoItem = await _repos.GetItemById(id);

            if (TodoItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(TodoItem TodoItem)
        {
            _repos.Update(TodoItem);

            return RedirectToPage("./Index");
        }

        private async Task<bool> PersonExists(Guid id)
        {
            TodoItem p = await _repos.GetItemById(id);

            return p != null;
        }
    }
}
