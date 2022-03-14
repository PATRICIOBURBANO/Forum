using Microsoft.AspNetCore.Identity;

namespace Forum.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Question> Questions { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; } = null!;
        public virtual ICollection<UserTag> UserTags { get; set; } = null!;
        public int Reputation { get; set; } = 0;

        public ApplicationUser()
        {
            Questions = new HashSet<Question>();
            Answers = new HashSet<Answer>();
            UserTags = new HashSet<UserTag>();
        }

    }
}
