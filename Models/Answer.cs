using System.ComponentModel.DataAnnotations;


namespace Forum.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime DateAnswer { get; set; } = DateTime.Now;
        public string BodyAnswer { get; set; } = null!;
        public int QuestionId { get; set; }
        public string? UserId { get; set; }
        //public int AnswerId { get; set; }
        public int VoteAnswer { get; set; }
        public bool IsItMostCorrect { get; set; } = false;
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }

        public Answer()
        {
            Answers = new List<Answer>(); 
            //User = new ApplicationUser();
        }

    }

    
}

