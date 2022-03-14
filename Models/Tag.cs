namespace Forum.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ICollection<UserTag> UserTags { get; set; }

        public Tag()
        {
            UserTags = new HashSet<UserTag>();
        }

    }
}
