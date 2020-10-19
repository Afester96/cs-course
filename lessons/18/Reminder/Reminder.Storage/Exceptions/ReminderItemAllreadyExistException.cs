using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Storage.Exceptions
{
    public class ReminderItemAllreadyExistException : Exception
	{
    
		public Guid Id { get; }

		public ReminderItemAllreadyExistException(Guid id) :
			base($"Reminder item with id {id:N} allready exist")
		{
			Id = id;
		}
	}
}
