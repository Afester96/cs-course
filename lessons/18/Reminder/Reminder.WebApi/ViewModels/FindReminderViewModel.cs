using Reminder.Storage;
using System;

namespace Reminder.WebApi.ViewModels
{
	public class FindReminderViewModel
	{
		public static FindReminderViewModel Default { get; } =
			new FindReminderViewModel
			{
				DateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				Status = ReminderItemStatus.Created,
			};

		public ReminderItemStatus? Status { get; set; }
		public long? DateTime { get; set; }

		public DateTimeOffset? DateTimeConverted =>
			DateTime.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(DateTime.Value) : default(DateTimeOffset?);
	}
}
