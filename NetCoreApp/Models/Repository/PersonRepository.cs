using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace NetCoreApp.Models.Repository
{
	public class PersonRepository : BaseRepository, IPersonRepository
	{
		public PersonRepository(IConfiguration config): base(config) { }
		
		public async Task<IEnumerable<DataModel.Person>> GetPersons()
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<DataModel.Person>(@"
select	person,
		name
from	person
");
			}
		}
	}
}