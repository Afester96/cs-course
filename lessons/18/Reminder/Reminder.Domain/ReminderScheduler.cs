using System;
using System.Threading;
using Microsoft.Extensions.Logging;

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

		private readonly ILogger _logger;
		private readonly IReminderStorage _storage;
		private readonly IReminderSender _sender;
		private readonly IReminderReceiver _receiver;
		private Timer _timer;

		public bool IsDisposed =>
			_timer == null;

		public ReminderScheduler(
			ILogger<ReminderScheduler> logger,
			IReminderStorage storage,
			IReminderSender sender,
			IReminderReceiver receiver)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_storage = storage ?? throw new ArgumentNullException(nameof(storage));
			_sender = sender ?? throw new ArgumentNullException(nameof(sender));
			_receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
		}

		public void Start(ReminderSchedulerSettings settings)
		{
			_logger.LogInformation("Starting reminders scheduling");
			_timer = new Timer(OnTimerTick, null, settings.TimerDelay, settings.TimerInterval);
			_receiver.MessageReceived += OnMessageReceived;
			_logger.LogInformation("Started reminders scheduling");
		}

		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}

			_logger.LogInformation("Stopping reminders scheduling");
			_receiver.MessageReceived -= OnMessageReceived;
			_timer.Dispose();
			_timer = null;
			_logger.LogInformation("Stopped reminders scheduling");
		}

		private void OnTimerTick(object state)
		{
			_logger.LogDebug("Ticked timer");

			var datetime = DateTimeOffset.UtcNow;
			var reminders = _storage.FindBy(ReminderItemFilter.CreatedAt(datetime));

			foreach (var reminder in reminders)
			{
				_logger.LogInformation($"Mark reminder {reminder.Id:N} as ready");
				reminder.MarkReady();

				try
				{
					_logger.LogInformation($"Sending reminder {reminder.Id:N}");
					_sender.Send(
						new ReminderNotification(
							reminder.ContactId,
							reminder.Message,
							reminder.DateTime
						)
					);
					OnReminderSent(reminder);
				}
				catch (ReminderSenderException exception)
				{
					_logger.LogError(exception, "Exception occured while sending notification");
					OnReminderFailed(reminder);
				}
			}
		}

		private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
		{
			_logger.LogDebug("Received message from receiver");

			var item = new ReminderItem(
				Guid.NewGuid(),
				ReminderItemStatus.Created,
				args.Message.DateTime,
				args.Message.Text,
				args.ContactId
			);
			_storage.Add(item);
			_logger.LogInformation($"Created reminder {item.Id:N}");
		}

		private void OnReminderSent(ReminderItem reminder)
		{
			_logger.LogInformation($"Mark reminder {reminder.Id:N} as sent");
			reminder.MarkSent();
			ReminderSent?.Invoke(this, new ReminderEventArgs(reminder));
		}

		private void OnReminderFailed(ReminderItem reminder)
		{
			_logger.LogWarning($"Mark reminder {reminder.Id:N} as failed");
			reminder.MarkFailed();
			ReminderFailed?.Invoke(this, new ReminderEventArgs(reminder));
		}
	}
}