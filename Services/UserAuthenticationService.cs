using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TaskManagement.Web.Helpers;
using TaskManagement.Web.Data.Models;
using System.Security.Claims;
using TaskManagement.Web.Data.Providers;

namespace TaskManagement.Web.Services
{

    public interface IUserAuthenticationService
    {
        bool TryGetLoginUser(User_Login userLogin, out USR_Users user);
        void SignInUser(USR_Users user);
    }
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserDataProvider _userDataProvider;
        private static IHttpContextAccessor _httpContextAccessor;
        public UserAuthenticationService(IUserDataProvider userDataProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userDataProvider = userDataProvider;
            _httpContextAccessor = httpContextAccessor;
        }              

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool TryGetLoginUser(User_Login userLogin, out USR_Users user)
        {
            user = null;

            if (userLogin != null &&
                !string.IsNullOrEmpty(userLogin.LoginID) &&
                !string.IsNullOrEmpty(userLogin.Password))
            {
                user = _userDataProvider.CheckUser(userLogin);
                if (user != null && PasswordHelper.VerifyEncryptedPassword(user.Password, userLogin.Password))
                    return true;
            }

            return false;
        }

        public void SignInUser(USR_Users user)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(type: "ImageExtn", value: string.Empty)
            };                

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                RedirectUri = "/Account/Login"
            };

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),authProperties);
        }
    }


}
