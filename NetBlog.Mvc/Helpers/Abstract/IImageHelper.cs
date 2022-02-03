using Microsoft.AspNetCore.Http;
using NetBlog.Entities.Dtos;
using NetBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadedUserImage(string username,IFormFile picturefile,string folderName = "userImages");
        IDataResult<ImageDeletedDto> Detele(string pictureName);
    }
}
