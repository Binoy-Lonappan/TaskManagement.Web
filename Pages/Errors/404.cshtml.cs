using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShiftManagement.WebPortal.Pages.Errors
{
    [AllowAnonymous]
    public class Error404Model : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
