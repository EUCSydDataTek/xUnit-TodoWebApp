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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRepository _repos;

        public IndexModel(ILogger<IndexModel> logger, IRepository repos)
        {
            _logger = logger;
            _repos = repos;
        }

        [BindProperty]
        public List<TodoItem> TodoItems { get; set; }

        [BindProperty]
        public PriorityLevel Priority { get; set; }

        public async Task OnGet()
        {
            TodoItems = await _repos.GetAll();
        }

        public async Task OnPost()
        {
            //foreach (TodoItem item in TodoItems)
            //{
            //    if (item.IsCompleted)
            //    {
            //        _repos.UpdateIsDone(item);
            //    }
            //}
            _repos.UpdateIsDone(TodoItems);
            TodoItems = await _repos.GetAll();
        }
    }
}
