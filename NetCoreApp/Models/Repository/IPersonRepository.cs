using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApp.Models.DataModel;

namespace NetCoreApp.Models.Repository
{
	public interface IPersonRepository
	{
		Task<IEnumerable<Person>> GetPersons();
	}
}