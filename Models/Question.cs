namespace Forum.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string TitleQuestion { get; set; } = null!;
        public string BodyQuestion { get; set; } = null!;
        public DateTime? DateQuestion { get; set; } = null;
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
        public string? UserId { get; set; } = null;
        public int VoteQuestion { get; set; } = 0;
        public int AmountAnswers { get; set; } = 0;
        public string Topic { get; set; } = null!;  
        public TagsQuestion TagsQuestion { get; set; }
        
        public Question()
        { 
        Answers = new List<Answer>();   
        }
    }
        public enum TagsQuestion
        {
            Software,
            Hardware,
            Network
        }
    }
