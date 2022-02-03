using AutoMapper;
using NetBlog.Data.Abstract;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using NetBlog.Services.Abstract;
using NetBlog.Shared.Utilities.Results.Abstract;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using NetBlog.Shared.Utilities.Results.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Services.Concreate
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await _unitOfWork.Comments.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Success, "hata", -1);
            }
        }

        public async Task<IDataResult<int>> CountByIsDeleted()
        {
            var categoriesCount = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Success, "hata", -1);
            }
        }
    }
}
