using System;

namespace Reminder.Storage.WebApi.Dto
{
	public class ReminderItemDto
	{
		public Guid Id { get; set; }
		public long DateTime { get; set; }
		public ReminderItemStatus Status { get; set; }
		public string Message { get; set; }
		public string ContactId { get; set; }

		public ReminderItemDto()
		{
		}

		public ReminderItemDto(ReminderItem item)
		{
			Id = item.Id;
			DateTime = item.DateTime.ToUnixTimeMilliseconds();
			Status = item.Status;
			Message = item.Message;
			ContactId = item.ContactId;
		}

		public ReminderItem ToItem() =>
			new ReminderItem(
				Id,
				Status,
				DateTimeOffset.FromUnixTimeMilliseconds(DateTime),
				Message,
				ContactId
			);
	}
}
