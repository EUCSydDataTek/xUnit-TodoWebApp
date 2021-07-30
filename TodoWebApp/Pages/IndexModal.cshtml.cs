// https://www.learnrazorpages.com/razor-pages/forms/checkboxes
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApp.Services;
using TodoWebApp.Data;

namespace TodoWebApp.Pages
{
    public class IndexModalModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITodoService _service;

        public IndexModalModel(ILogger<IndexModel> logger, ITodoService service)
        {
            _logger = logger;
            _service = service;
        }

        [BindProperty]
        public List<TodoItem> TodoItems { get; set; }

        [BindProperty]
        public PriorityLevel Priority { get; set; }

        public async Task OnGet()
        {
            TodoItems = await _service.GetAll();
        }

        public async Task OnPost()
        {
            TodoItem item = TodoItems.FirstOrDefault(t => t.IsCompleted == true);

            await _service.UpdateIsDone(item);
            TodoItems = await _service.GetAll();
        }

        // Modal
        [BindProperty]
        public TodoItem TodoItem { get; set; }

        public IActionResult OnPostModalAdd()
        {
            if (ModelState.IsValid)
            {
                _service.Insert(TodoItem);
                return RedirectToPage("./IndexModal");
            }

            return Page();
        }
    }
}

