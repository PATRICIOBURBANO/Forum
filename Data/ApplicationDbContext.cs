using Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

namespace Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Forum.Models.Answer> Answer { get; set; }
        public DbSet<Forum.Models.Question> Question { get; set; }
        

        public DbSet<Tag> Tag { get; set; }
        public DbSet<UserTag> UserTag { get; set; }

    }
}