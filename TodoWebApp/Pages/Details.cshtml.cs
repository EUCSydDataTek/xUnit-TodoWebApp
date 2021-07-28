using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ITodoService _repos;

        public DetailsModel(ITodoService repos)
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

            if (TodoItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
