using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Services
{
    public interface IFileService
    {
        string UploadFile(IFormFile image, IWebHostEnvironment webHostEnvironment);
    }

    public class FileService : IFileService
    {
        public string UploadFile(IFormFile image, IWebHostEnvironment webHostEnvironment)
        {
            string fileName = null;

            if (image != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
