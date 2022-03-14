namespace Forum.Models
{
    public class UserTag
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int TagId{ get; set; }
        public Tag Tag { get; set; }
         

        public ApplicationUser? User { get; set; }

        public UserTag()
        { 
        
        }

    }
}
