using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Reminder.Domain
{
	using Receiver;
	using Sender;
	using Sender.Exceptions;
	using Storage;

	public class ReminderScheduler
	{
		public event EventHandler<ReminderEventArgs> ReminderSent;
		public event EventHandler<ReminderEventArgs> ReminderFailed;

		private readonly ILogger _logger;
		private readonly IReminderStorage _storage;
		private readonly IReminderSender _sender;
		private readonly IReminderReceiver _receiver;

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

		public async Task StartAsync(
			ReminderSchedulerSettings settings,
			CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Starting reminders scheduling");
			_receiver.MessageReceived += OnMessageReceived;

			await Task.Delay(settings.TimerDelay, cancellationToken);
			_logger.LogInformation("Started reminders scheduling");

			while (!cancellationToken.IsCancellationRequested)
			{
				await OnTickAsync();
				await Task.Delay(settings.TimerInterval, cancellationToken);
			}
		}

		private async Task OnTickAsync()
		{
			_logger.LogDebug("Ticked timer");

			foreach (var reminder in await _storage.FindByAsync(ReminderItemFilter.CreatedAtNow()))
			{
				_logger.LogInformation($"Mark reminder {reminder.Id:N} as ready");
				reminder.MarkReady();
				await _storage.UpdateAsync(reminder);

				try
				{
					_logger.LogInformation($"Sending reminder {reminder.Id:N}");
					await _sender.SendAsync(
						new ReminderNotification(
							reminder.ContactId,
							reminder.Message,
							reminder.DateTime
						)
					);
					await OnReminderSentAsync(reminder);
				}
				catch (ReminderSenderException exception)
				{
					_logger.LogError(exception, "Exception occured while sending notification");
					await OnReminderFailedAsync(reminder);
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
			_ = _storage.AddAsync(item);
			_logger.LogInformation($"Created reminder {item.Id:N}");
		}

		private async Task OnReminderSentAsync(ReminderItem reminder)
		{
			_logger.LogInformation($"Mark reminder {reminder.Id:N} as sent");
			reminder.MarkSent();
			await _storage.UpdateAsync(reminder);
			ReminderSent?.Invoke(this, new ReminderEventArgs(reminder));
		}

		private async Task OnReminderFailedAsync(ReminderItem reminder)
		{
			_logger.LogWarning($"Mark reminder {reminder.Id:N} as failed");
			reminder.MarkFailed();
			await _storage.UpdateAsync(reminder);
			ReminderFailed?.Invoke(this, new ReminderEventArgs(reminder));
		}
	}
}