using System;
using System.Threading;
using Reminder.Domain;
using Reminder.Storage;
using Reminder.Storage.Memory;

namespace Reminder
{
	class Program
	{
		static void Main(string[] args)
		{
			using var scheduler = new ReminderScheduler(
				new ReminderStorage(
					new ReminderItem(
						Guid.NewGuid(),
						ReminderItemStatus.Created,
						DateTimeOffset.UtcNow.AddSeconds(10),
						"Message 1",
						"Contact 1"),
					new ReminderItem(
						Guid.NewGuid(),
						ReminderItemStatus.Created,
						DateTimeOffset.UtcNow.AddMinutes(1),
						"Message 2",
						"Contact 2")
				), null
			);
			scheduler.ReminderSent += OnReminderSent;
			scheduler.Start(new ReminderSchedulerSettings());
			Console.WriteLine("Waiting reminders..");
			Console.WriteLine("Press any key to stop");
			Console.ReadKey();
		}

		private static void OnReminderSent(object sender, ReminderSentEventArgs args)
		{
			Console.WriteLine(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent received with " +
				$"message {args.Reminder.Message}"
			);
			Thread.Sleep(5 * 1000); // Send to TG latency
		}
	}
}