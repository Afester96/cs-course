using System;
using Reminder.Storage;

namespace Reminder.Tests
{
	public class ReminderItemBuilder
	{
		private Guid _id = Guid.NewGuid();
		private ReminderItemStatus _status = ReminderItemStatus.Created;
		private DateTimeOffset _datetime = DateTimeOffset.UtcNow;
		private string _message;
		private string _contact;

		public ReminderItemBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ReminderItemBuilder WithMessage(string message)
		{
			_message = message;
			return this;
		}

		public ReminderItemBuilder WithContact(string contact)
		{
			_contact = contact;
			return this;
		}

		public ReminderItemBuilder AtDatetime(DateTimeOffset datetime)
		{
			_datetime = datetime;
			return this;
		}

		public ReminderItemBuilder AtUtcNow() =>
			AtDatetime(DateTimeOffset.UtcNow);

		public ReminderItem Please() => 
			new ReminderItem(_id, _status, _datetime, _message, _contact);

		public static implicit operator ReminderItem(ReminderItemBuilder builder) =>
			builder.Please();
	}
}