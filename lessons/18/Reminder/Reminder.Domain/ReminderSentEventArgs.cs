using Reminder.Storage;

namespace Reminder.Domain
{
	public class ReminderSentEventArgs
	{
		public ReminderItem Reminder { get; }

		public ReminderSentEventArgs(ReminderItem reminder)
		{
			Reminder = reminder;
		}
	}
}