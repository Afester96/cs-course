using System;
using Reminder.Storage;

namespace Reminder.WebApi.ViewModels
{
	public class GetReminderViewModel
	{
		public Guid Id { get; }
		public long DateTime { get; }
		public string Status { get; }
		public string Message { get; }
		public string ContactId { get; }

		public GetReminderViewModel(ReminderItem item)
		{
			Id = item.Id;
			DateTime = item.DateTime.ToUnixTimeMilliseconds();
			Status = item.Status.ToString();
			Message = item.Message;
			ContactId = item.ContactId;
		}
	}
}