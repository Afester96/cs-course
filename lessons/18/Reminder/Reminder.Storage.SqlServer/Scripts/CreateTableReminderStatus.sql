CREATE TABLE [ReminderStatus] (
    [Id] INT NOT NULL,
    [Name] VARCHAR(32) NOT NULL,

    CONSTRAINT PK_ReminderStatusId PRIMARY KEY ([Id]),
    CONSTRAINT UQ_ReminderStatusName UNIQUE ([Name])
);