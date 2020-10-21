using System;
using System.Threading;
using Reminder.Sender;
using Reminder.Storage;

namespace Reminder.Domain
{
	public class ReminderScheduler : IDisposable
	{
		public event EventHandler<ReminderSentEventArgs> ReminderSent;

		private readonly IReminderStorage _storage;
		private readonly IReminderSender _sender;
		private Timer _timer;

		public bool IsDisposed =>
			_timer == null;

		public ReminderScheduler(
			IReminderStorage storage,
			IReminderSender sender)
		{
			_storage = storage ?? throw new ArgumentNullException(nameof(storage));
			_sender = sender ?? throw new ArgumentNullException(nameof(sender));
		}

		public void Start(ReminderSchedulerSettings settings)
		{
			_timer = new Timer(OnTimerTick, null, settings.TimerDelay, settings.TimerInterval);
		}

		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}

			_timer.Dispose();
			_timer = null;
		}

		private void OnTimerTick(object state)
		{
			var datetime = DateTimeOffset.UtcNow;
			var reminders = _storage.Find(datetime);

			foreach (var reminder in reminders)
			{
				reminder.Status = ReminderItemStatus.Ready;
				_sender.Send(
					new ReminderNotification(
						reminder.ContactId, 
						reminder.Message, 
						reminder.DateTime
					)
				);
				reminder.MarkSent();
				ReminderSent?.Invoke(this, new ReminderSentEventArgs(reminder));
			}
		}
	}
}