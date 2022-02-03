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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto category, string CreatedByName)
        {

            var categorys = _mapper.Map<Category>(category);
            categorys.CreatedByName = CreatedByName;
            categorys.ModifiedByName = CreatedByName;
            var addedCategory = await _unitOfWork.Categories.AddAync(categorys);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.Name} adlı kategori başarı ile eklenmiştir.", new CategoryDto { 
                    Category= addedCategory,
                    ResultStatus=ResultStatus.Success,
                    Message= $"{category.Name} adlı kategori başarı ile eklenmiştir."
            });
        }

        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();
            if (categoriesCount>-1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Success,"hata" ,-1);
            }
        }

        public async Task<IDataResult<int>> CountByIsDeleted()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c => !c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Success, "hata", -1);
            }
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryID, string ModifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryID);
            if (category != null)
            {
                
                category.IsDeleted = true;
                category.ModifiedByName = ModifiedByName;
                category.ModifiedOn = DateTime.Now;

                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name} adlı kategori başarı ile eklenmiştir.", new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedCategory.Name} adlı kategori başarı ile silinmiştir."
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir kategori bulunamadı"
            });
            
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
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryDto {Category= null,
                ResultStatus=ResultStatus.Error,
                Message= "Böyle bir kategori bulunamadı"
            });
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
                    ResultStatus = ResultStatus.Success,
                    Message = "Kategori Listelendi"
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir kategori bulunamadı"
            });
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

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryID)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c=>c.Id==categoryID);

            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c=>c.Id==categoryID);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);

                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
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

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categorydto, string ModifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c=>c.Id== categorydto.Id);
            var categorys = _mapper.Map<CategoryUpdateDto,Category>(categorydto, oldCategory);
            categorys.ModifiedByName = ModifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(categorys);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categorys.Name} adlı kategori başarı ile eklenmiştir.", new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categorys.Name} adlı kategori başarı ile eklenmiştir."
            });
        }

       
    }
}
