using Forum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Models
{
    public class    SeedData
    {

        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            if (!context.Question.Any())
            {

                List<Question> newCourses = new List<Question>()
                {
                new Question {TitleQuestion = "My first question",
                              BodyQuestion = "Why are studying C#?",
                              DateQuestion = DateTime.Now
                              },
                new Question { TitleQuestion = "My second question",
                               BodyQuestion = "How do you like Html?",
                               DateQuestion = DateTime.Now
                },
                new Question {TitleQuestion = "My third question",
                              BodyQuestion = "Are you strugling with JavaScript?",
                              DateQuestion = DateTime.Now
                },
                };

                context.Question.AddRange(newCourses);
            }

            if (!context.Roles.Any())
            {
                List<string> newRoles = new List<string>()
            {
            "Administrator",
            "Instructor",
            "Student",
            "User",
            "Guest"

            };
                foreach (string role in newRoles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

            }

            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                ApplicationUser firstAdmin = new ApplicationUser
                {
                    Email = "admin1@mitt.ca",
                    NormalizedEmail = "ADMIN1@MITT.CA",
                    UserName = "admin1@mitt.ca",
                    NormalizedUserName = "ADMIN1@MITT.CA",
                    EmailConfirmed = true
                };
                ApplicationUser secondAdmin = new ApplicationUser
                {
                    Email = "admin1@mitt.ca",
                    NormalizedEmail = "ADMIN1@MITT.CA",
                    UserName = "admin1@mitt.ca",
                    NormalizedUserName = "ADMIN1@MITT.CA",
                    EmailConfirmed = true
                };
                ApplicationUser thirdAdmin = new ApplicationUser
                {
                    Email = "admin1@mitt.ca",
                    NormalizedEmail = "ADMIN1@MITT.CA",
                    UserName = "admin1@mitt.ca",
                    NormalizedUserName = "ADMIN1@MITT.CA",
                    EmailConfirmed = true
                };
                var hashedPassword = passwordHasher.HashPassword(firstAdmin, "P@ssword1");
                var hashedPassword2 = passwordHasher.HashPassword(secondAdmin, "P@ssword1");
                var hashedPassword3 = passwordHasher.HashPassword(thirdAdmin, "P@ssword1");
                firstAdmin.PasswordHash = hashedPassword;
                secondAdmin.PasswordHash = hashedPassword2;
                thirdAdmin.PasswordHash = hashedPassword3;


                await userManager.CreateAsync(firstAdmin);
                await userManager.CreateAsync(secondAdmin);
                await userManager.CreateAsync(thirdAdmin);


                await userManager.AddToRoleAsync(firstAdmin, "Administrator");
                await userManager.AddToRoleAsync(secondAdmin, "Administrator");
                await userManager.AddToRoleAsync(thirdAdmin, "Administrator");

            }


            context.SaveChanges();
        }
    }
}
