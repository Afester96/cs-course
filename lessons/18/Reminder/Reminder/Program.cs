using System;
using Reminder.Domain;
using Reminder.Receiver.Telegram;
using Reminder.Sender.Telegram;
using Reminder.Storage.Memory;


namespace Reminder
{
	class Program
	{
		private const string TelegramToken = "1194138212:AAECudhNDJkrmuVxhHSCzixX7MAq-jRrMtI";

		static void Main(string[] args)
		{
			using var scheduler = new ReminderScheduler(
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
			WriteLine("Waiting reminders..", ConsoleColor.Green);
			WriteLine("Press any key to stop", ConsoleColor.Yellow);
			Console.ReadKey();
		}

		private static void OnReminderSent(object sender, ReminderEventArgs args) =>
			WriteLine(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent received with " +
				$"message {args.Reminder.Message}", ConsoleColor.Green
			);

		private static void OnReminderFailed(object sender, ReminderEventArgs args) =>
			WriteLine(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent failed with " +
				$"message {args.Reminder.Message}", ConsoleColor.Red
			);

		private static void WriteLine(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}