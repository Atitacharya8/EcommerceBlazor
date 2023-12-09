using EcommerceBlazorWebApp_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace EcommerceBlazorWebApp_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string filePath)
        {
            if(File.Exists(_webHostEnvironment.WebRootPath + filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath + filePath);
                return true;
            }

            return false;

        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name); //this is build in feature that have fileinfo services
             //create the filename with guid and the type of file (jpg, png, etc)
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;

            //this will get the access and get the path of webroot folder
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images\\product";

            if(!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            var filePath = Path.Combine(folderDirectory, fileName);

            // the file will be received and pasted at filestream "fs" that we created
            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);//we will receive the file in filestream

            var fullPath = $"/images/product/{fileName}";
            return fullPath;
        }
    }
}
