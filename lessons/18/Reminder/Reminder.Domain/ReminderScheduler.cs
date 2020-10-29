using System;
using System.Threading;

namespace Reminder.Domain
{
	using Receiver;
	using Sender;
	using Sender.Exceptions;
	using Storage;

	public class ReminderScheduler : IDisposable
	{
		public event EventHandler<ReminderEventArgs> ReminderSent;
		public event EventHandler<ReminderEventArgs> ReminderFailed;

		private readonly IReminderStorage _storage;
		private readonly IReminderSender _sender;
		private readonly IReminderReceiver _receiver;
		private Timer _timer;

		public bool IsDisposed =>
			_timer == null;

		public ReminderScheduler(
			IReminderStorage storage,
			IReminderSender sender,
			IReminderReceiver receiver)
		{
			_storage = storage ?? throw new ArgumentNullException(nameof(storage));
			_sender = sender ?? throw new ArgumentNullException(nameof(sender));
			_receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
		}

		public void Start(ReminderSchedulerSettings settings)
		{
			_timer = new Timer(OnTimerTick, null, settings.TimerDelay, settings.TimerInterval);
			_receiver.MessageReceived += OnMessageReceived;
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
				reminder.MarkReady();

				try
				{
					_sender.Send(
						new ReminderNotification(
							reminder.ContactId,
							reminder.Message,
							reminder.DateTime
						)
					);
					OnReminderSent(reminder);
				}
				catch (ReminderSenderException)
				{
					OnReminderFailed(reminder);
				}
			}
		}

		private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
		{
			var item = new ReminderItem(
				Guid.NewGuid(),
				ReminderItemStatus.Created,
				args.Message.DateTime,
				args.Message.Text,
				args.ContactId
			);
			_storage.Add(item);
		}

		private void OnReminderSent(ReminderItem reminder)
		{
			reminder.MarkSent();
			ReminderSent?.Invoke(this, new ReminderEventArgs(reminder));
		}

		private void OnReminderFailed(ReminderItem reminder)
		{
			reminder.MarkFailed();
			ReminderFailed?.Invoke(this, new ReminderEventArgs(reminder));
		}
	}
}