using System;
using System.Collections.Generic;

namespace HomeWork22.Storage
{
	public interface IStorage
	{
		void Add(City city);
		void Delete(City city);
		void Update(City city);
		City Get(Guid id);
	}
}