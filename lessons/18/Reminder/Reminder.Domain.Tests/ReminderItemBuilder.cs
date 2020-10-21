using System;
using Reminder.Storage;

namespace Reminder.Domain.Tests
{
	public class ReminderItemBuilder
	{
		private Guid _id;
		private ReminderItemStatus _status = ReminderItemStatus.Created;
		private DateTimeOffset _datetime = DateTimeOffset.UtcNow;
		private string _message;
		private string _contact;

		public ReminderItemBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ReminderItemBuilder AtUtcNow()
		{
			return this;
		}

		public static implicit operator ReminderItem(ReminderItemBuilder builder)
		{
			return new ReminderItem(
				builder._id,
				builder._status,
				builder._datetime,
				builder._message,
				builder._contact);
		}
	}
}