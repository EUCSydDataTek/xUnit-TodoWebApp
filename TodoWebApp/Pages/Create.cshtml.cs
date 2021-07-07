using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _context;

        public CreateModel(IRepository context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoItem TodoItem { get; set; }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Insert(TodoItem);
                return RedirectToPage("./Index");
            }

            return Page();           
        }
    }
}
