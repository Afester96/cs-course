CREATE TABLE [ReminderContact] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [ChatId] VARCHAR(32) NOT NULL,

    CONSTRAINT PK_ReminderContactId PRIMARY KEY ([Id]),
    CONSTRAINT UQ_ReminderContactChatId UNIQUE ([ChatId])
);