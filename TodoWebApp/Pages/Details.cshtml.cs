using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository _context;

        public DetailsModel(IRepository context)
        {
            _context = context;
        }

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
    }
}
