using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using NetBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleID);

        Task<IDataResult<ArticleListDto>> GetAll();

        Task<IDataResult<ArticleListDto>> GetAllByNonDeleted();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedandActive();

        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryID);

        Task<IResult> Add(ArticleAddDto category, string CreatedByName);

        Task<IResult> Update(ArticleUpdateDto category, string ModifiedByName);
        Task<IResult> Delete(int articleID, string ModifiedByName);

        Task<IResult> HardDelete(int articleID);

        Task<IDataResult<int>> Count();

        Task<IDataResult<int>> CountByIsDeleted();
    }
}
