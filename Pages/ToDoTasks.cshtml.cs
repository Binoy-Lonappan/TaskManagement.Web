using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Data.Models;

namespace TaskManagement.Web.Pages
{
    public class ToDoTasksModel : PageModel
    {
        [BindProperty]
        public TasksToDo tasksToDo { get; set; }
        public void OnGet()
        {
            tasksToDo = new TasksToDo();

        }
    }
}
