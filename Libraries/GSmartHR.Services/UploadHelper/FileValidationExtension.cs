using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GSmartHR.Services.UploadHelper
{
    public static class FileValidationExtension
    {
        public static bool ValidateImageFile(IFormFile file)
        {
            if (file == null)
                return false;

            var fileName = Path.GetFileName(file.FileName);
            string extension = (Path.GetExtension(fileName)).ToLower();

            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
            {
                return true;
            }

            return false;
        }
        public static bool ValidateDocAndPdfFile(IFormFile file)
        {
            if (file == null)
                return false;

            var fileName = Path.GetFileName(file.FileName);
            string extension = (Path.GetExtension(fileName)).ToLower();

            if (extension == ".pdf" || extension == ".docx" || extension == ".doc")
            {
                return true;
            }

            return false;
        }
        public static bool ValidatePdfFile(IFormFile file)
        {
            if (file == null)
                return false;

            var fileName = Path.GetFileName(file.FileName);
            string extension = (Path.GetExtension(fileName)).ToLower();

            if (extension == ".pdf")
            {
                return true;
            }

            return false;


        }
        public static bool ValidateSize(IFormFile file, int mb)
        {
            if (file == null)
                return false;
            if (file.Length < mb * 1024 * 1024)
            {
                return true;
            }
            return false;
        }

        public static bool AllowImageToDelete(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            if (fileName.ToLower().Contains("slider"))
                return false;

            return true;
        }

    }
}
