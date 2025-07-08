using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_ITI_DB.Models;

public partial class dbContext : IdentityDbContext<ApplicationUser>
{
    public dbContext()
    {
    }

    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Exam> Exams { get; set; }
    public virtual DbSet<ExamAssignment> ExamAssignments { get; set; }
    public virtual DbSet<ExamResult> ExamResults { get; set; }
    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<UserSubject> UserSubjects { get; set; }
    public virtual DbSet<YearLevel> YearLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This must come FIRST to configure Identity tables
        base.OnModelCreating(modelBuilder);


        // Your existing configurations
        modelBuilder.Entity<Exam>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DurationMinutes).HasComment("Must be greater than 0");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");

            entity.HasOne(e => e.CreatedBy)
                .WithMany(u => u.CreatedExams)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.Subject)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.SubjectId);

            entity.HasOne(e => e.YearLevel)
                .WithMany(y => y.Exams)
                .HasForeignKey(e => e.YearLevelId);
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PassThreshold).HasDefaultValue(60.0m); // Default pass threshold of 60%

            entity.HasOne(e => e.Exam)
                  .WithMany(e => e.ExamResults)
                  .HasForeignKey(e => e.ExamId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                  .WithMany(u => u.ExamResults)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Points).HasDefaultValue(1); // Default points per question

            entity.HasOne(q => q.Exam)
                  .WithMany(e => e.Questions)
                  .HasForeignKey(q => q.ExamId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<StudentAnswer>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsCorrect).HasDefaultValue(false);

            entity.HasOne(sa => sa.Question)
                  .WithMany(q => q.StudentAnswers)
                  .HasForeignKey(sa => sa.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sa => sa.ExamResult)
                  .WithMany(er => er.StudentAnswers)
                  .HasForeignKey(sa => sa.ExamResultId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sa => sa.User)
                  .WithMany(u => u.StudentAnswers)
                  .HasForeignKey(sa => sa.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(s => s.IsActive).HasDefaultValue(true);
        });
        modelBuilder.Entity<UserSubject>(entity =>
        {
            entity.Property(us => us.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(us => us.User)
                  .WithMany(u => u.UserSubjects)
                  .HasForeignKey(us => us.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(us => us.Subject)
                  .WithMany(s => s.UserSubjects)
                  .HasForeignKey(us => us.SubjectId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<YearLevel>(entity =>
        {
            entity.Property(yl => yl.IsActive).HasDefaultValue(true);
        });

        // Configure ApplicationUser (inherits from IdentityUser)
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(u => u.YearLevel)
                  .WithMany(y => y.Users)
                  .HasForeignKey(u => u.YearLevelId);
            // Relationship to UserRoles
            entity.HasMany(e => e.UserRoles)
                  .WithOne()
                  .HasForeignKey(e => e.UserId)
                  .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}