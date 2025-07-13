namespace Backend.DTO
{
    public class QuestionData
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string difficulty { get; set; }
        public int points { get; set; }
        public List<String> options { get; set; }
        public string correctAnswer { get; set; }
    }
}
