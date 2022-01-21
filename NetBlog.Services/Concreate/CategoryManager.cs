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

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IResult> Add(CategoryAddDto category, string CreatedByName)
        {
            await _unitOfWork.Categories.AddAync(new Category { 
                Name = category.Name,
                Description= category.Description,
                Note= category.Note,
                IsActive=category.IsActive,
                CreatedByName=CreatedByName,
                CreatedOn=DateTime.Now,
                ModifiedByName=CreatedByName,
                ModifiedOn=DateTime.Now,
                IsDeleted=false
            }).ContinueWith(t=> _unitOfWork.SaveAsync());
            //await _unitOfWork.SaveAsync();
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

                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarı ile silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<Category>> Get(int categoryID)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryID, a => a.Articles);
            if(category!= null)
            {
                return new DataResult<Category>(ResultStatus.Success, category);
            }
            return new DataResult<Category>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            var category = await _unitOfWork.Categories.GetAllAsync(null,c=>c.Articles);
            if (category.Count>-1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success,category);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAllByNonDeleted()
        {
            var category = await _unitOfWork.Categories.GetAllAsync(c=>!c.IsDeleted, c => c.Articles);
            if (category.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, category);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int categoryID)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryID);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarı ile silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IResult> Update(CategoryUpdateDto categorydto, string ModifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c=>c.Id== categorydto.Id);
            if (category != null)
            {
                category.Name = categorydto.Name;
                category.Description = categorydto.Description;
                category.Note = categorydto.Note;
                category.IsActive = categorydto.IsActive;
                category.IsDeleted = false;
                category.ModifiedByName = ModifiedByName;
                category.ModifiedOn = DateTime.Now;

                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t=>_unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categorydto.Name} adlı kategori başarı ile güncellenmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }
    }
}
