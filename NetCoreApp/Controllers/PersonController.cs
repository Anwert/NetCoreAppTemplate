using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models.Service;

namespace NetCoreApp.Controllers
{
	public class PersonController : Controller
	{
		private readonly IPersonService _personService;

		public PersonController(IPersonService person_service)
		{
			_personService = person_service;
		}

		public async Task<ActionResult> Index()
		{
			var persons = await _personService.Get();
			return View(persons);
		}
	}
}