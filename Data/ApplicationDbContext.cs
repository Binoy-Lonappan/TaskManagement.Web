
using Microsoft.EntityFrameworkCore;
using TaskManagement.Web.Data.Models;

namespace TaskManagement.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User_Login>().Metadata.SetIsTableExcludedFromMigrations(true);
            builder.Entity<UserSignUp>().Metadata.SetIsTableExcludedFromMigrations(true);
            //builder.Entity<User_Login>().ToTable("User_Login", t => t.ExcludeFromMigrations());
            //builder.Entity<UserSignUp>().ToTable("UserSignUp", t => t.ExcludeFromMigrations());
            base.OnModelCreating(builder);

            // Calling seed method to create default user
            SeedUsersAndRoles seedUsersRoles = new();
            builder.Entity<USR_Users>().HasData(seedUsersRoles.Users);
        }

        public DbSet<TasksToDo> TasksToDo { get; set; }
        public DbSet<USR_Users> USR_Users { get; set; }
    }
}
