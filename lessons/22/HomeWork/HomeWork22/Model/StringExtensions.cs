using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22.Model
{
	public static class StringExtensions
	{
		public static string Capitalize(this string title)
		{
			return char.ToUpper(title[0]) + title.Substring(1).ToLower();
		}
	}
}
