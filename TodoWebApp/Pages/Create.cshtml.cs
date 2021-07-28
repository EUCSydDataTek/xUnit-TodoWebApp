using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _repos;

        public CreateModel(IRepository repos)
        {
            _repos = repos;
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
                await _repos.Insert(TodoItem);
                return RedirectToPage("./Index");
            }

            return Page();           
        }
    }
}
