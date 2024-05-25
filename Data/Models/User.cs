
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Web.Data.Models
{
    [NotMapped]
    [Keyless]
    public class User_Login
    {        
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }

    public class USR_Users
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }

    [NotMapped]
    [Keyless]
    public class UserSignUp
    {        
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }              

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
               
    }
}
