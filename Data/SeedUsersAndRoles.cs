
using TaskManagement.Web.Data.Models;
using TaskManagement.Web.Helpers;

namespace TaskManagement.Web.Data
{
    public class SeedUsersAndRoles
    {
        private readonly List<USR_Users> _users;

        public SeedUsersAndRoles()
        {
            _users = GetUsers();
        }
        public List<USR_Users> Users { get { return _users; } }              

        private List<USR_Users> GetUsers()
        {

            string pwd = "123";           
            
            // Seed Users
            var adminUser = new USR_Users
            {
                Id = Guid.NewGuid(),
                LoginID = "admin",
                UserName = "admin",
                Password = PasswordHelper.EncryptPassword(pwd)
        };

            List<USR_Users> users = new List<USR_Users>() {
                adminUser
            };

            return users;
        }
               
    }
}