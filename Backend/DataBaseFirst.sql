CREATE TABLE [YearLevels] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(50),
  [Code] varchar(20),
  [Description] varchar(255),
  [IsActive] bit DEFAULT 1
)
GO

CREATE TABLE [Subjects] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(100),
  [Description] text,
  [IsActive] bit DEFAULT 1
)
GO

CREATE TABLE [Roles] (
  [Id] nvarchar(450) PRIMARY KEY,
  [Name] nvarchar(256),
  [NormalizedName] nvarchar(256),
  [ConcurrencyStamp] nvarchar(max)
)
GO

CREATE TABLE [Users] (
  [Id] nvarchar(450) PRIMARY KEY,
  [UserName] nvarchar(256),
  [NormalizedUserName] nvarchar(256),
  [Email] nvarchar(256),
  [NormalizedEmail] nvarchar(256),
  [EmailConfirmed] bit DEFAULT 0,
  [PasswordHash] nvarchar(max),
  [SecurityStamp] nvarchar(max),
  [ConcurrencyStamp] nvarchar(max),
  [PhoneNumber] nvarchar(max),
  [PhoneNumberConfirmed] bit DEFAULT 0,
  [TwoFactorEnabled] bit DEFAULT 0,
  [LockoutEnd] datetimeoffset,
  [LockoutEnabled] bit DEFAULT 0,
  [AccessFailedCount] int DEFAULT 0,
  [FullName] nvarchar(100),
  [IsAdmin] bit DEFAULT 0,
  [IsTeacher] bit DEFAULT 0,
  [YearLevelId] int,
  [CreatedDate] datetime2 DEFAULT GETDATE(),
  [LastUpdated] datetime2 DEFAULT GETDATE(),
  FOREIGN KEY ([YearLevelId]) REFERENCES [YearLevels] ([Id])
)
GO

CREATE TABLE [UserSubjects] (
  [UserId] nvarchar(450),
  [SubjectId] int,
  [CreatedDate] datetime2 DEFAULT GETDATE(),
  PRIMARY KEY ([UserId], [SubjectId]),
  FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]),
  FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id])
)
GO

CREATE TABLE [Exams] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] nvarchar(100),
  [Description] nvarchar(max),
  [DurationMinutes] int CHECK ([DurationMinutes] > 0),
  [IsActive] bit DEFAULT 1,
  [SubjectId] int,
  [YearLevelId] int,
  [CreatedById] nvarchar(450),
  [CreatedDate] datetime2 DEFAULT GETDATE(),
  [LastUpdated] datetime2 DEFAULT GETDATE(),
  FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]),
  FOREIGN KEY ([YearLevelId]) REFERENCES [YearLevels] ([Id]),
  FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id])
)
GO

CREATE TABLE [Questions] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Text] nvarchar(max),
  [Options] nvarchar(max),
  [CorrectAnswer] nvarchar(255),
  [Points] int DEFAULT 1 CHECK ([Points] > 0),
  [ExamId] int,
  [CreatedDate] datetime2 DEFAULT GETDATE(),
  [LastUpdated] datetime2 DEFAULT GETDATE(),
  FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id])
)
GO

CREATE TABLE [ExamAssignments] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ExamId] int,
  [UserId] nvarchar(450),
  [AssignedDate] datetime2 DEFAULT GETDATE(),
  [DueDate] datetime2,
  [IsCompleted] bit DEFAULT 0,
  [CompletedDate] datetime2,
  FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id]),
  FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]),
  CONSTRAINT [CHK_DueDateAfterAssigned] CHECK ([DueDate] > [AssignedDate])
)
GO

CREATE UNIQUE INDEX [IX_ExamAssignments_ExamId_UserId] ON [ExamAssignments] ([ExamId], [UserId])
GO

CREATE TABLE [ExamResults] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] nvarchar(450),
  [ExamId] int,
  [Score] int,
  [TotalPoints] int,
  [Percentage] decimal(5,2),
  [StartTime] datetime2,
  [EndTime] datetime2,
  [DurationMinutes] int,
  [IsPassed] bit,
  [PassThreshold] decimal(5,2) DEFAULT 70.00,
  [CreatedDate] datetime2 DEFAULT GETDATE(),
  FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]),
  FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id]),
  CONSTRAINT [CHK_EndTimeAfterStartTime] CHECK ([EndTime] > [StartTime])
)
GO

CREATE TABLE [StudentAnswers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] nvarchar(450),
  [QuestionId] int,
  [ExamResultId] int,
  [Answer] nvarchar(255),
  [IsCorrect] bit,
  [PointsEarned] int DEFAULT 0,
  [CreatedDate] datetime2 DEFAULT GETDATE(),
  FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]),
  FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]),
  FOREIGN KEY ([ExamResultId]) REFERENCES [ExamResults] ([Id])
)
GO

CREATE TABLE [UserRoles] (
  [UserId] nvarchar(450),
  [RoleId] nvarchar(450),
  PRIMARY KEY ([UserId], [RoleId]),
  FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]),
  FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
)
GO

-- Add descriptions as extended properties
EXEC sp_addextendedproperty 
@name = N'MS_Description', 
@value = 'Must be greater than 0',
@level0type = N'SCHEMA', @level0name = 'dbo',
@level1type = N'TABLE', @level1name = 'Exams',
@level2type = N'COLUMN', @level2name = 'DurationMinutes'
GO

EXEC sp_addextendedproperty 
@name = N'MS_Description', 
@value = 'Must be greater than 0',
@level0type = N'SCHEMA', @level0name = 'dbo',
@level1type = N'TABLE', @level1name = 'Questions',
@level2type = N'COLUMN', @level2name = 'Points'
GO

EXEC sp_addextendedproperty 
@name = N'MS_Description', 
@value = 'Calculated as (Score/TotalPoints)*100',
@level0type = N'SCHEMA', @level0name = 'dbo',
@level1type = N'TABLE', @level1name = 'ExamResults',
@level2type = N'COLUMN', @level2name = 'Percentage'
GO

EXEC sp_addextendedproperty 
@name = N'MS_Description', 
@value = 'True when Percentage >= PassThreshold',
@level0type = N'SCHEMA', @level0name = 'dbo',
@level1type = N'TABLE', @level1name = 'ExamResults',
@level2type = N'COLUMN', @level2name = 'IsPassed'
GO

EXEC sp_addextendedproperty 
@name = N'MS_Description', 
@value = 'Cannot exceed Question.Points',
@level0type = N'SCHEMA', @level0name = 'dbo',
@level1type = N'TABLE', @level1name = 'StudentAnswers',
@level2type = N'COLUMN', @level2name = 'PointsEarned'
GO

