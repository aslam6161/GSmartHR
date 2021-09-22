using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Services.UploadHelper
{
    public class FileUploadService : IFileUploadService
    {
        private string Directory { get; set; }


        public void LoadDirectory(string directory)
        {
            var filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", directory);

            Directory = filePath;

            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
        }

        public string UploadFile(IFormFile file, string fileName = null)
        {
            try
            {
                if (file != null)
                {
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    }

                    var path = Path.Combine(Directory, fileName);

                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                             file.CopyTo(stream);
                        }
                    }

                    return fileName;
                }

            }
            catch (Exception e)
            {

            }

            return null;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    return false;
                var path = Path.Combine(Directory, fileName);

                FileInfo file = new FileInfo(path);

                if (file.Exists)
                {
                    file.Delete();
                    return true;
                }
            }
            catch (Exception e)
            {

            }


            return false;
        }

        public string GetFileName(IFormFile formFile)
        {
            var path = Path.Combine(Directory, formFile.FileName);

            if(!File.Exists(path))
            {
                return formFile.FileName;
            }
            
            return Guid.NewGuid() + "."+ Path.GetExtension(formFile.FileName);
        }
    }
}
