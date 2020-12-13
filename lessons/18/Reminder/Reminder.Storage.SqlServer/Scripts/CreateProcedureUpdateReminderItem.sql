CREATE PROCEDURE dbo.UpdateReminderItem
    @id UNIQUEIDENTIFIER,
    @statusId INT,
    @message NVARCHAR(512),
    @rows INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [ReminderItem] 
       SET StatusId = @statusId,
           Message = @message
     WHERE Id = @id;
    SET @rows = @@ROWCOUNT;
END