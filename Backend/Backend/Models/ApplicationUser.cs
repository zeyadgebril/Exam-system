using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebAPI_ITI_DB.Models;

namespace Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; } = false;
        public bool IsTeacher { get; set; } = false;

        public int? YearLevelId { get; set; }
        public virtual YearLevel YearLevel { get; set; }

        // Navigation properties
        public virtual ICollection<Exam> CreatedExams { get; set; } = new List<Exam>();
        public virtual ICollection<ExamAssignment> ExamAssignments { get; set; } = new List<ExamAssignment>();
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
        public virtual ICollection<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}