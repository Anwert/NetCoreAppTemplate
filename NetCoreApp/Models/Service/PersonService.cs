using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApp.Models.DataModel;
using NetCoreApp.Models.Repository;

namespace NetCoreApp.Models.Service
{
	public class PersonService : IPersonService
	{
		private readonly IPersonRepository _personRepository;

		public PersonService(IPersonRepository person_repository)
		{
			_personRepository = person_repository;
		}
		
		public async Task<IEnumerable<Person>> Get()
		{
			return await _personRepository.GetPersons();
		}
	}
}