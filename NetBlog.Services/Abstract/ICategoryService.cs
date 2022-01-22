﻿using NetBlog.Entities.Concreate;
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

        Task<IDataResult<CategoryListDto>> GetAll();

        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();

        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedandActive();

        Task<IResult> Add(CategoryAddDto category,string CreatedByName);

        Task<IResult> Update(CategoryUpdateDto category,string ModifiedByName);
        Task<IResult> Delete(int categoryID, string ModifiedByName);

        Task<IResult> HardDelete(int categoryID);
    }

}
