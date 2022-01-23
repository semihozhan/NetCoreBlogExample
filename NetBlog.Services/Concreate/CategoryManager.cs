﻿using AutoMapper;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> Add(CategoryAddDto category, string CreatedByName)
        {

            var categorys = _mapper.Map<Category>(category);
            categorys.CreatedByName = CreatedByName;
            categorys.ModifiedByName = CreatedByName;
            await _unitOfWork.Categories.AddAync(categorys);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarı ile eklenmiştir.");
        }

        public async Task<IResult> Delete(int categoryID, string ModifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryID);
            if (category != null)
            {
                
                category.IsDeleted = true;
                category.ModifiedByName = ModifiedByName;
                category.ModifiedOn = DateTime.Now;

                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarı ile silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryID)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryID, a => a.Articles);
            if(category!= null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto {
                    Category=category,
                    ResultStatus=ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var category = await _unitOfWork.Categories.GetAllAsync(null,c=>c.Articles);
            if (category.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = category,
                    ResultStatus = ResultStatus.Success,
                    Message = "Kategori Listelendi"
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryListDto {
            Categories=null,
            ResultStatus=ResultStatus.Error,
            Message= "Böyle bir kategori bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var category = await _unitOfWork.Categories.GetAllAsync(c=>!c.IsDeleted, c => c.Articles);
            if (category.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedandActive()
        {
            var category = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted&&c.IsActive, c => c.Articles);
            if (category.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int categoryID)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryID);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarı ile silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IResult> Update(CategoryUpdateDto categorydto, string ModifiedByName)
        {
            var categorys = _mapper.Map<Category>(categorydto);
            categorys.ModifiedByName = ModifiedByName;
            await _unitOfWork.Categories.UpdateAsync(categorys);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{categorydto.Name} adlı kategori başarı ile güncellenmiştir.");
        }

       
    }
}