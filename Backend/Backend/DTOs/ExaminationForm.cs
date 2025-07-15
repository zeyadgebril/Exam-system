namespace Backend.DTO
{
    public class ExaminationForm
    {
        public string ExamName { get; set; }
        public int? ExamDuration { get; set; }
        public int? YearLevel { get; set; }
        public int? Subject { get; set; }
        public List<QuestionData> QuestionData { get; set; }
    }
}
