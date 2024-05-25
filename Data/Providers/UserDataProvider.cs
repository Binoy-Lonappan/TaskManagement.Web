using TaskManagement.Web.Data;
using TaskManagement.Web.Data.Models;
using TaskManagement.Web.Helpers;

namespace TaskManagement.Web.Data.Providers
{
    public interface IUserDataProvider
    {
        USR_Users CheckUser(User_Login userLogin);
        List<USR_Users> GetUsers(bool includeAdminUsers = false);
        USR_Users CreateNewUser(USR_Users user,out bool IsSuccess, out string errormsg);
        void SaveUser(USR_Users user);
        void UpdateUserStatus(Guid id, bool deleted);
       
       
    }
    public class UserDataProvider : IUserDataProvider
    {
        private readonly ApplicationDbContext _context;

        public UserDataProvider(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public USR_Users CheckUser(User_Login userLogin)
        {
            var user = _context.USR_Users.SingleOrDefault(u => u.LoginID == userLogin.LoginID);
            return user;
        }
        public List<USR_Users> GetUsers(bool includeAdminUsers = false)
        {
            return _context.USR_Users
                .OrderBy(x => x.UserName)
                .ToList(); 
        }

        public USR_Users CreateNewUser(USR_Users user, out bool IsSuccess, out string errormsg)
        {
            errormsg = string.Empty;
            IsSuccess = false;
            if (user != null)
            {
                var existuser = _context.USR_Users.Where(x => x.LoginID == user.LoginID).SingleOrDefault();
                if(existuser == null)
                {
                    // Encrypt password
                    user.Password = PasswordHelper.EncryptPassword(user.Password);
                    _context.USR_Users.Add(user);
                    _context.SaveChanges();
                    IsSuccess = true;
                    return user;
                }
                else
                {
                    errormsg = "LoginID already in use. Please enter another loginId.";
                    return user;
                }                
            }
            else
            {
                errormsg = "Invalid details entered.";
                return user;
            }

        }


        public void SaveUser(USR_Users user)
        {
            var userUpdate = _context.USR_Users.SingleOrDefault(x => x.Id == user.Id);
            if (userUpdate == null)
                _context.Add(user);
            else
            {
                userUpdate.UserName = user.UserName;
                userUpdate.Password = user.Password;
            }
            _context.SaveChanges();
        }
        
        public void UpdateUserStatus(Guid id, bool deleted)
        {
            var userToDelete = _context.USR_Users.SingleOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();            
            _context.SaveChanges();
        }

      

    }
}
