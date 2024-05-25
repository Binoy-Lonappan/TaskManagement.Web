using TaskManagement.Web.Data.Models;
using TaskManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace TaskManagement.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserAuthenticationService _userAuthentication;

        public LoginModel(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        public void OnGet()
        {           
            LoginUser = new User_Login();
        }

        [BindProperty]
        public User_Login LoginUser { get; set; }

        public IActionResult OnPost(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.Page("/Index");
           
            var isValidLogin = _userAuthentication.TryGetLoginUser(LoginUser, out USR_Users user);

            if (!isValidLogin)
                ModelState.AddModelError("Username", "Incorrect User Name or Password");            
            else
            {
                _userAuthentication.SignInUser(user);
				return Redirect(Url.Page("/Index"));
			}
            return Page();
        }

        
    }
}
