using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreApp.Models.Service
{
	public class ImageService : IImageService
	{
		public async Task<string> UploadImage(IFormFile file)
		{
			var file_bytes = ConvertToBytes(file);
			if (CheckIfImageFile(file_bytes))
			{
				return ConvertToBase64(file_bytes);
			}

			return "Invalid image file";
		}
		
		
		private ImageFormat GetImageFormat(byte[] bytes)
		{
			var bmp = Encoding.ASCII.GetBytes("BM");               // BMP
			var gif = Encoding.ASCII.GetBytes("GIF");              // GIF
			var png = new byte[] { 137, 80, 78, 71 };              // PNG
			var tiff = new byte[] { 73, 73, 42 };                  // TIFF
			var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
			var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
			var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon

			if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
				return ImageFormat.bmp;

			if (gif.SequenceEqual(bytes.Take(gif.Length)))
				return ImageFormat.gif;

			if (png.SequenceEqual(bytes.Take(png.Length)))
				return ImageFormat.png;

			if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
				return ImageFormat.tiff;

			if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
				return ImageFormat.tiff;

			if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
				return ImageFormat.jpeg;

			if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
				return ImageFormat.jpeg;

			return ImageFormat.unknown;
		}

		/// <summary>
		/// Method to check if file is image file
		/// </summary>
		private bool CheckIfImageFile(byte[] file_bytes)
		{
			return GetImageFormat(file_bytes) != ImageFormat.unknown;
		}

		private byte[] ConvertToBytes(IFormFile file)
		{
			byte[] file_bytes;
			using (var ms = new MemoryStream())
			{
				file.CopyTo(ms);
				file_bytes = ms.ToArray();
			}

			return file_bytes;
		}

		private string ConvertToBase64(byte[] file_bytes)
		{
			return Convert.ToBase64String(file_bytes);
		}

		/// <summary>
		/// Method to write file onto the disk
		/// </summary>
		private async Task<string> WriteFile(IFormFile file)
		{
			string file_name;
			try
			{
				var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
				file_name = Guid.NewGuid().ToString() + extension; //Create a new Name 
				//for the file due to security reasons.
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file_name);

				using (var bits = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(bits);
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}

			return file_name;
		}
	}
}