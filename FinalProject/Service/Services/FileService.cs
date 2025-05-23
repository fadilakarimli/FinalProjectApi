using Microsoft.AspNetCore.Http;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Service.Services
{
    public class FileService : IFileService
    {
        public void Delete(string fileName, string folder)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public async Task<string> UploadFilesAsync(IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, fileName);
            using (FileStream stream = new(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
