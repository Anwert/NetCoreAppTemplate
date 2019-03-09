using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreApp.Models.Service
{
	public interface IImageService
	{
		Task<string> UploadImage(IFormFile file);
	}
}