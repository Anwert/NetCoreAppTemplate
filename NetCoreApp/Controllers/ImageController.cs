using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models.Service;

namespace NetCoreApp.Controllers
{
	public class ImageController : Controller
	{
		private readonly IImageService _imageService;

		public ImageController(IImageService image_service)
		{
			_imageService = image_service;
		}
		
		public IActionResult Index()
		{
			return View();
		}
		
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			var result = await _imageService.UploadImage(file);
			return new ObjectResult(result);
		}
	}
}