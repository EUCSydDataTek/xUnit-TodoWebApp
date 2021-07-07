using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRepository _context;

        public EditModel(IRepository context)
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
}
