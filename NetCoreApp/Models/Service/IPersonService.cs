using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApp.Models.DataModel;

namespace NetCoreApp.Models.Service
{
	public interface IPersonService
	{
		Task<IEnumerable<Person>> Get();
	}
}