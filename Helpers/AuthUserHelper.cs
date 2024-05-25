using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace TaskManagement.Web.Helpers
{
    public static class AuthUserHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static Guid LoggedInUserId
        {
            get
            {
                string? userId = null;
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;
                if (userClaims != null)
                {
                    userId = userClaims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
                }
                return new Guid(userId);
            }
        }

        //public static bool IsAdminUserLoggedIn
        //{
        //    get
        //    {
        //        if (_httpContextAccessor.HttpContext.User.Claims != null)
        //        {
        //            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
        //            if (userClaims != null)
        //            {
        //                return userClaims.Single(x => x.Type == ClaimTypes.Role).Value == "Administrator";
        //            }

        //        }
        //        return false;
        //    }
        //}
    }
}
