IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Users].[name]', N'Name', N'COLUMN';
GO

ALTER TABLE [Users] ADD [rol] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [DeletedBy] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [ShortDescription] nvarchar(280) NOT NULL,
    [GeneralDescription] nvarchar(max) NOT NULL,
    [Level] int NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [DeletedBy] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Dob] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [DeletedBy] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Chapters] (
    [Id] int NOT NULL IDENTITY,
    [CourseId] int NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [DeletedBy] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Chapters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Chapters_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CourseCategory] (
    [CategoriesId] int NOT NULL,
    [CoursesId] int NOT NULL,
    CONSTRAINT [PK_CourseCategory] PRIMARY KEY ([CategoriesId], [CoursesId]),
    CONSTRAINT [FK_CourseCategory_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CourseCategory_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CourseStudent] (
    [CoursesId] int NOT NULL,
    [studentsId] int NOT NULL,
    CONSTRAINT [PK_CourseStudent] PRIMARY KEY ([CoursesId], [studentsId]),
    CONSTRAINT [FK_CourseStudent_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CourseStudent_Students_studentsId] FOREIGN KEY ([studentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Chapters_CourseId] ON [Chapters] ([CourseId]);
GO

CREATE INDEX [IX_CourseCategory_CoursesId] ON [CourseCategory] ([CoursesId]);
GO

CREATE INDEX [IX_CourseStudent_studentsId] ON [CourseStudent] ([studentsId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240514035313_create tables', N'8.0.4');
GO

COMMIT;
GO

