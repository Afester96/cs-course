using System;

namespace HomeWork22.City
{
	public class City : ICity
	{
		public City(Guid id, string title, string description, int population)
		{
			Id = id;
			Title = title;
			Description = description;
			Population = population;
		}

		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Population { get; set; }
	}
}