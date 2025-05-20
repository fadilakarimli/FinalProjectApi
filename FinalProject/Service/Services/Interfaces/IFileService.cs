using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFilesAsync(IFormFile file, string folder);
        void Delete(string fileName, string folder);
    }
}
