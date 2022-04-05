namespace Forum.Models
{
    public class Vote
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? QuestionId { get; set; } = null!;
        public int? AnswerId { get; set; } = null!;
        public int? QuestionVote { get; set; } = null!;
        public int? AnswerVote { get; set; } = null!;
        public int? UserReputation { get; set; } = null!;

        public Vote ()
        {


        }
    }
}
