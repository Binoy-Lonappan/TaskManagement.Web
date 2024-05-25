using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Data.Models;
using TaskManagement.Web.Data.Providers;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Pages.Account
{
    public class SignupModel : PageModel
    {
        private readonly IUserAuthenticationService _userAuthentication;
        private readonly IUserDataProvider _userDataProvider;

        public SignupModel(IUserDataProvider userDataProvider, IUserAuthenticationService userAuthentication)
        {
            _userDataProvider = userDataProvider;
            _userAuthentication = userAuthentication;
        }
        [BindProperty]
        public UserSignUp SignupUser { get; set; }
        public void OnGet()
        {
            SignupUser = new UserSignUp();
        }

        public IActionResult OnPost(UserSignUp SignupUser)
        {

            if(ModelState.IsValid)
            {
                USR_Users newuser = new USR_Users() {
                    UserName = SignupUser.UserName,
                    Password = SignupUser.Password,
                };

                // Create new user
                string errormsg = string.Empty;
                bool IsSuccess = false;
                var newsuccessuser = _userDataProvider.CreateNewUser(newuser,out IsSuccess, out errormsg);
                if(newsuccessuser != null)
                {
                    if(IsSuccess)
                    {
                        // if success signin user and redirect to Admin page
                        _userAuthentication.SignInUser(newsuccessuser);
                        return Redirect(Url.Page("/Index"));
                    }
                    else
                    { // else show error message
                        ModelState.AddModelError("Username", errormsg);
                    }
                }
                else
                { // else show error message
                    ModelState.AddModelError("Username", errormsg);
                }
            }
            else
            {
                ModelState.AddModelError("Username", "Invalid details entered. Please check."); 
            }
            return Page();
        }
       
    }
}
