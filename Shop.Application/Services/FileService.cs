using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Services
{
    public interface IFileService
    {
        string UploadFile(IFormFile image);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string UploadFile(IFormFile image)
        {
            string fileName = null;

            if (image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
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
