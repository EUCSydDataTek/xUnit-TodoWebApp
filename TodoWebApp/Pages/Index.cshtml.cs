// https://www.learnrazorpages.com/razor-pages/forms/checkboxes
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRepository _context;

        public IndexModel(ILogger<IndexModel> logger, IRepository context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public List<TodoItem> TodoItems { get; set; }

        [BindProperty]
        public PriorityLevel Priority { get; set; }

        public void OnGet()
        {
            TodoItems = _context.GetAll();
        }

        public void OnPost()
        {
            foreach (TodoItem item in TodoItems)
            {
                if (item.IsCompleted)
                {
                    _context.UpdateIsDone(item);
                }
            }
            TodoItems = _context.GetAll();
        }
    }
}
