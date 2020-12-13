using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace Reminder.Storage.SqlServer
{
	public static class ReminderStorageScripts
	{
		public static string CreateProcedureAddReminderItem =>
			ReadScript(nameof(CreateProcedureAddReminderItem));

		public static string CreateProcedureUpdateReminderItem =>
			ReadScript(nameof(CreateProcedureUpdateReminderItem));

		public static string CreateTableReminderContact =>
			ReadScript(nameof(CreateTableReminderContact));

		public static string CreateTableReminderItem =>
			ReadScript(nameof(CreateTableReminderItem));

		public static string CreateTableReminderStatus =>
			ReadScript(nameof(CreateTableReminderStatus));

		public static string InsertReminderStatus =>
			ReadScript(nameof(InsertReminderStatus));

		private static string ReadScript(string script)
		{
			var provider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
			var file = provider.GetFileInfo($"Scripts\\{script}.sql");
			using var reader = new StreamReader(file.CreateReadStream());
			return reader.ReadToEnd();
		}
	}
}