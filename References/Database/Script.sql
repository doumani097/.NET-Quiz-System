CREATE TABLE [Users] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserName] nvarchar(255),
  [FirstName] nvarchar(255),
  [LastName] nvarchar(255),
  [Email] nvarchar(255),
  [Password] nvarchar(255),
  [Created_at] timestamp
)
GO

CREATE TABLE [UserContacts] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Photo] text,
  [PhoneNumber1] nvarchar(255),
  [PhoneNumber2] nvarchar(255),
  [Country] nvarchar(255),
  [City] nvarchar(255),
  [Street] nvarchar(255),
  [Appartment] nvarchar(255),
  [ZipCode] nvarchar(255),
  [UserId] int
)
GO

CREATE TABLE [Exams] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [ShortDescription] nvarchar(255),
  [Description] text,
  [Image] text,
  [Created_at] timestamp
)
GO

CREATE TABLE [UserExams] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int,
  [ExamId] int,
  [Created_at] timestamp
)
GO

CREATE TABLE [Questions] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Number] int,
  [Name] nvarchar(255),
  [Mark] double,
  [Created_at] timestamp,
  [ExamId] int
)
GO

CREATE TABLE [Answers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [IsCorrect] bool,
  [Created_at] timestamp,
  [QuestionId] int
)
GO

CREATE TABLE [UserAnswers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [IsCorrect] bool,
  [UserId] int,
  [AnswerId] int,
  [Created_at] timestamp
)
GO

ALTER TABLE [UserContacts] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [UserExams] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [UserExams] ADD FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id])
GO

ALTER TABLE [Questions] ADD FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id])
GO

ALTER TABLE [Answers] ADD FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id])
GO

ALTER TABLE [UserAnswers] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [UserAnswers] ADD FOREIGN KEY ([AnswerId]) REFERENCES [Answers] ([Id])
GO
