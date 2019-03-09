using System.Collections.Generic;
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
		
		public async Task<IActionResult> UploadImage(List<IFormFile> files)
		{
			var results = new List<byte[]>();
			foreach (var file in files)
			{
				var result = await _imageService.UploadImage(file);
				results.Add(result);
			}

			return RedirectToAction("Index");
		}
	}
}