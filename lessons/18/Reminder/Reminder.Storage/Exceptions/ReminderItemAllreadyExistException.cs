using System;

namespace Reminder.Storage.Exceptions
{
    public class ReminderItemAllreadyExistException : Exception
	{
		public Guid Id { get; }

		public ReminderItemAllreadyExistException(Guid id, Exception exception = default) :
			base($"Reminder item with id {id:N} allready exist", exception)
		{
			Id = id;
		}
	}
}
