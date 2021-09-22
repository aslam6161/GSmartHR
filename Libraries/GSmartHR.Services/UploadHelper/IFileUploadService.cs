using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GSmartHR.Services.UploadHelper
{
    public interface IFileUploadService
    {
        void LoadDirectory(string directory);
        string UploadFile(IFormFile formFile, string fileName = null);
        bool DeleteFile(string fileName);
        string GetFileName(IFormFile formFile);
    }
}
