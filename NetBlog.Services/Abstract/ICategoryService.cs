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
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int categoryID);

        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryID);

        Task<IDataResult<CategoryListDto>> GetAll();

        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();

        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedandActive();
        /// <summary>
        /// Kategori ekler
        /// </summary>
        /// <param name="category">dto nesnesi</param>
        /// <returns>geriye dto tipinde dataresult döner </returns>
        Task<IDataResult<CategoryDto>> Add(CategoryAddDto category,string CreatedByName);

        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto category,string ModifiedByName);
        Task<IDataResult<CategoryDto>> Delete(int categoryID, string ModifiedByName);

        Task<IResult> HardDelete(int categoryID);

        Task<IDataResult<int>> Count();

        Task<IDataResult<int>> CountByIsDeleted();
    }

}
