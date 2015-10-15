using System;
using bybus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bybus.Services
{
	public interface IRepository<T> where T : Entity
	{
		Task<IList<T>> Search (string search);

		Task<IEnumerable<T>> All();
	}
}