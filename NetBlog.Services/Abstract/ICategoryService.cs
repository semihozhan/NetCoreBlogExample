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
        Task<IDataResult<Category>> Get(int categoryID);

        Task<IDataResult<IList<Category>>> GetAll();

        Task<IResult> Add(CategoryAddDto category,string CreatedByName);

        Task<IResult> Update(CategoryUpdateDto category,string ModifiedByName);
        Task<IResult> Delete(int categoryID);

        Task<IResult> HardDelete(int categoryID);
    }

}
