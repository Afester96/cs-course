CREATE PROCEDURE dbo.AddReminderItem
    @id UNIQUEIDENTIFIER,
    @dateTime DATETIMEOFFSET,
    @statusId INT,
    @message NVARCHAR(512),
    @chatId VARCHAR(32)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @contactId UNIQUEIDENTIFIER;
    SELECT @contactId = Id FROM [ReminderContact] WHERE [ChatId] = @chatId;

    IF @contactId IS NULL
    BEGIN
        SET @contactId = NEWID();
        INSERT INTO [ReminderContact]([Id], [ChatId]) 
        VALUES (@contactId, @chatId);
    END
    
    INSERT INTO [ReminderItem]([Id], [DateTime], [StatusId], [Message], [ContactId]) 
    VALUES (@id, @dateTime, @statusId, @message, @contactId);
END