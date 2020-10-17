using System;
using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
	// FizzBaz
	// Написать метод, который возвращает строку в формате:
	// Метод принимает аргумент (целое число)
	// Вернуть Fizz если число делится на 3
	// Вернуть Buzz если яисло делится на 5
	// Вернуть FizzBuzz если число делится на 15
	// В противном случае, вернуть переданное число как строку

	public class FizzBuzzTests
	{
		[Test]
		public void GivenThree_ShouldReturnFizz() => Assert.AreEqual("Fizz", FizzBuzz(3));

		[Test]
		public void GivenNine_ShouldReturnFizz() => Assert.AreEqual("Fizz", FizzBuzz(9));

		[Test]
		public void GivenTwenty_ShouldReturnBuzz() => Assert.AreEqual("Buzz", FizzBuzz(20));

		[Test]
		public void GivenThirteen_ShouldReturn13() => Assert.AreEqual("13", FizzBuzz(13));

		[Test]
		public void GivenFitheen_ShouldReturnFizzBuzz() => Assert.AreEqual("FizzBuzz", FizzBuzz(15));

		[TestCase(3, "Fizz")]
		[TestCase(9, "Fizz")]
		[TestCase(20, "Buzz")]
		[TestCase(13, "13")]
		[TestCase(15, "FizzBuzz")]
		public void GivenNumber_ShouldReturnExpectedString(int number, string expected)
		{
			Assert.AreEqual(expected, FizzBuzz(number));
		}

		public string FizzBuzz(int number) =>
			(number % 3 == 0, number % 5 == 0) switch
			{
				(true, true) => "FizzBuzz",
				(true, false) => "Fizz",
				(false, true) => "Buzz",
				(_, _) => number.ToString(),
			};

		// number % 15 == 0 ? "FizzBuzz" :
		// number % 3 == 0 ? "Fizz" :
		// number % 5 == 0 ? "Buzz" : number.ToString();
	}
}