using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository _context;

        public DeleteModel(IRepository context)
        {
            _context = context;
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TodoItem TodoItem = _context.GetItemById(id);

            if (TodoItem != null)
            {
                _context.Remove(TodoItem.Id);
                return RedirectToPage("./Index");             
            }
            return NotFound();
        }
    }
}
