using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace What_Should_I_eat.Services.Global
{
    public interface ISaveFileService
    {
         string SaveFile(IFormFile file);
    }


    public class SaveFileService : ISaveFileService
    {

        public string SaveFile(IFormFile file)
         {
            var fileName = System.IO.Path.GetFileName(file.FileName);
            var extension = fileName.Split('.').Last();
            var fileNameWithoutExtension = string.Join("", fileName.Split('.').Take(fileName.Length - 1));

            var newFileName = "wwwroot/images/" + String.Format("{0}-{1:ddMMYYYHHmmss}.{2}",
                fileNameWithoutExtension,
                DateTime.Now,
                extension);
            if (!Directory.Exists(Path.GetDirectoryName(newFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newFileName));
            }

            using (var localFile = System.IO.File.OpenWrite(newFileName))
            {
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }

            return newFileName;
        }

       
    }

}
