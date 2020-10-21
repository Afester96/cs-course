using System;
using System.Threading;
using NUnit.Framework;
using Reminder.Sender;

namespace Reminder.Domain.Tests
{
	public class ReminderSender : IReminderSender
	{
		public void Send(ReminderNotification item)
		{
			throw new Exception("");
		}
	}

	public class ReminderSchedulerTests
	{
		public ReminderSchedulerSettings DefaultSettings =>
			new ReminderSchedulerSettings
			{
				TimerDelay = TimeSpan.Zero,
				TimerInterval = TimeSpan.FromMilliseconds(20)
			};

		[Test]
		public void GivenReminderWithPastDate_ShouldRaiseRaised()
		{
			var raised = false;
			using var scheduler = new ReminderScheduler(
				Create.Storage
					.WithItems(Create.Reminder.AtUtcNow())
					.Build(),
				new ReminderSender()
			);
			scheduler.ReminderSent += (sender, args) => raised = true;

			scheduler.Start(DefaultSettings);
			WaitTimers();

			Assert.IsTrue(raised);
		}

		private void WaitTimers()
		{
			Thread.Sleep(DefaultSettings.TimerInterval.Milliseconds * 2);
		}
	}
}