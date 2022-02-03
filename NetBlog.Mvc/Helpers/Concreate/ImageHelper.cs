using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NetBlog.Entities.Dtos;
using NetBlog.Mvc.Helpers.Abstract;
using NetBlog.Shared.Utilities.Extensions;
using NetBlog.Shared.Utilities.Results.Abstract;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using NetBlog.Shared.Utilities.Results.Concreate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Helpers.Concreate
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private object pictureFile;
        private string wwwroot;

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        public IDataResult<ImageDeletedDto> Detele(string pictureName)
        {
             string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fileToDelete = Path.Combine($"{wwwroot}/img/", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    FullPath = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);

                return new DataResult<ImageDeletedDto>(
                    ResultStatus.Success,
                    imageDeletedDto);
            }
            return new DataResult<ImageDeletedDto>(
                    ResultStatus.Error,
                    "Hata",
                    null);
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadedUserImage(string username, IFormFile picturefile, string folderName="userImages")
        {
            if (!Directory.Exists($"{wwwroot}/img/{folderName}"))
            {
                Directory.CreateDirectory($"{wwwroot}/img/{folderName}");
            }

            string oldfileName = Path.GetFileNameWithoutExtension(picturefile.FileName);
            string fileExtension = Path.GetExtension(picturefile.FileName);
            DateTime dateTime = DateTime.Now;
            string newfileName = $"{username}_{DateTimeExtensions.FullDateandTimeStringwithUnderscore(dateTime)}{fileExtension}";

            var path = Path.Combine($"{wwwroot}/img/{folderName}", newfileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await picturefile.CopyToAsync(stream);
            }

            return new DataResult<ImageUploadedDto>(
                ResultStatus.Success,
                "Basarili", 
                new ImageUploadedDto{
                FullName = $"{folderName}/{newfileName}",
                OldName = oldfileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = picturefile.Length
            });
        }
    }
}
