using Employe.Models;
using Employe.ViewModels;
using Microsoft.AspNetCore.Hosting;
using NuGet.Packaging.Signing;

namespace Employe.Utilities
{
    public static class FileManager
    {
        public static string UpdloadImage(this IFormFile formFile, string envPath, string folder)
        {
            string fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            string extension = Path.GetExtension(formFile.FileName);

            string uploadPath = Path.Combine(envPath, folder);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            if (Path.Exists(Path.Combine(uploadPath, fileName + extension)))
            {
                fileName = fileName + Guid.NewGuid().ToString();
            }

            fileName = fileName + extension;
            uploadPath = Path.Combine(uploadPath, fileName);

            using FileStream fileStream = new FileStream(uploadPath, FileMode.Create);
            formFile.CopyTo(fileStream);
            return fileName;
        }

        public static bool CheckSize(this IFormFile formFile, int size)
        {
            if (formFile.Length > size * 1024 * 1024)
            {
                return false;
            }
            return true;
        }

        public static bool CheckType(this IFormFile formFile)
        {
            string extension = Path.GetExtension(formFile.FileName);
            string[] AllowFormat = [".png", ".jpg", ".jpeg"];
            bool isAlllowed = false;
            foreach (var item in AllowFormat)
            {
                if (item == extension)
                {
                    isAlllowed = true;
                    break;
                }
            }
            return isAlllowed;
        }

    }
}
