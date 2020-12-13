using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Reminder
{
	using Domain;
	using Receiver.Telegram;
	using Sender.Telegram;
	using Reminder.Storage.SqlServer;
    using System.Threading.Tasks;

    class Program
	{
		private const string TelegramToken = "1194138212:AAECudhNDJkrmuVxhHSCzixX7MAq-jRrMtI";

		private const string ConnectionString = "Server=tcp:shadow-art.database.windows.net,1433;Initial Catalog=reminder;Persist Security Info=False;Encrypt=True;";

		private static readonly ILoggerFactory Logging = LoggerFactory.Create(_ =>
		{
			_.AddConsole();
			_.SetMinimumLevel(LogLevel.Trace);
		}
		);

		private static readonly ILogger Logger = Logging.CreateLogger<Program>();
		private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

		static async Task Main()
		{
			var scheduler = new ReminderScheduler(
				Logging.CreateLogger<ReminderScheduler>(),
				new ReminderStorage(ConnectionString),
				new ReminderSender(TelegramToken),
				new ReminderReceiver(TelegramToken)
			);
			scheduler.ReminderSent += OnReminderSent;
			scheduler.ReminderFailed += OnReminderFailed;
			await scheduler.StartAsync(
				new ReminderSchedulerSettings
				{
					TimerDelay = TimeSpan.Zero,
					TimerInterval = TimeSpan.FromSeconds(1)
				},
				CancellationTokenSource.Token
			);
			Logger.LogInformation("Waiting reminders..");
			Logger.LogInformation("Press any key to stop");
			Console.ReadKey();
			CancellationTokenSource.Cancel();
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