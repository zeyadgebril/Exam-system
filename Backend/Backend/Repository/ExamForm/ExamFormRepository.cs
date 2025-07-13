using System.Runtime.Intrinsics.Arm;
using Backend.DTO;
using WebAPI_ITI_DB.Models;

namespace Backend.Repository.ExamForm
{
    public class ExamFormRepository : IExamFormRepository
    {
        private readonly dbContext db;

        public ExamFormRepository(dbContext db)
        {
            this.db = db;
        }
        public void add(ExaminationForm entity)
        {
            var QuestionList = new List<Question>();
            foreach(var item in entity.QuestionData)
            {
                var Question = new Question()
                {
                    Text = item.QuestionText,
                    Options = string.Join(", ", item.options).ToLower(),
                    CorrectAnswer = item.correctAnswer.ToLower(),
                    Points = item.points,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,

                };
                QuestionList.Add(Question);
                
            }
            var exam = new Exam()
            {
                Title = entity.ExamName,
                DurationMinutes = entity.ExamDuration,
                SubjectId = entity.Subject,
                YearLevelId = entity.YearLevel,
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
                Questions = QuestionList

            };
            db.Exams.Add(exam);

        }

        public void delete(ExaminationForm entity)
        {
            throw new NotImplementedException();
        }

        public List<ExaminationForm> getAll()
        {
            throw new NotImplementedException();
        }

        public ExaminationForm getById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ExaminationForm entity)
        {
            throw new NotImplementedException();
        }
    }
}
