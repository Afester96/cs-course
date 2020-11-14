using System;
using Microsoft.Extensions.Logging;
using Reminder.Domain;
using Reminder.Receiver.Telegram;
using Reminder.Sender.Telegram;
using Reminder.Storage.Memory;


namespace Reminder
{
	class Program
	{
		private const string TelegramToken = "1194138212:AAECudhNDJkrmuVxhHSCzixX7MAq-jRrMtI";

		private static readonly ILoggerFactory Logging = LoggerFactory.Create(_ =>
		{
			_.AddConsole();
			_.SetMinimumLevel(LogLevel.Trace);
		}
		);

		private static readonly ILogger Logger = Logging.CreateLogger<Program>();

		static void Main(string[] args)
		{
			using var scheduler = new ReminderScheduler(
				Logging.CreateLogger<ReminderScheduler>(),
				new ReminderStorage(),
				new ReminderSender(TelegramToken),
				new ReminderReceiver(TelegramToken)
			);
			scheduler.ReminderSent += OnReminderSent;
			scheduler.ReminderFailed += OnReminderFailed;
			scheduler.Start(
				new ReminderSchedulerSettings
				{
					TimerDelay = TimeSpan.Zero,
					TimerInterval = TimeSpan.FromSeconds(1)
				}
			);
			Logger.LogInformation("Waiting reminders..");
			Logger.LogInformation("Press any key to stop");
			Console.ReadKey();
		}

		private static void OnReminderSent(object sender, ReminderEventArgs args) =>
			Logger.LogInformation(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent received with " +
				$"message {args.Reminder.Message}"
			);

		private static void OnReminderFailed(object sender, ReminderEventArgs args) =>
			Logger.LogWarning(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent failed with " +
				$"message {args.Reminder.Message}"
			);
	}
}