// https://www.learnrazorpages.com/razor-pages/forms/checkboxes
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class IndexModalModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITodoService _service;

        public IndexModalModel(ILogger<IndexModel> logger, ITodoService repos)
        {
            _logger = logger;
            _service = repos;
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
            await _service.UpdateIsDone(TodoItems);
            TodoItems = await _service.GetAll();
        }
    }
}

