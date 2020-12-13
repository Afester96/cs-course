using System.Threading.Tasks;
using Reminder.Sender;
using Reminder.Sender.Exceptions;

namespace Reminder.Domain.Tests
{
	public class ReminderSender : IReminderSender
	{
		private readonly bool _fail;

		public ReminderSender(bool fail)
		{
			_fail = fail;
		}

		public Task SendAsync(ReminderNotification item) =>
			_fail ? Task.FromException(new ReminderSenderException(null)) : Task.CompletedTask;
	}
}