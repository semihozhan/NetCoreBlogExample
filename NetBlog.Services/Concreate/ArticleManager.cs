using AutoMapper;
using NetBlog.Data.Abstract;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using NetBlog.Services.Abstract;
using NetBlog.Services.AutoMapper.Profiles;
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
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ArticleManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto category, string CreatedByName)
        {
            var article = _mapper.Map<Article>(category);
            article.CreatedByName = CreatedByName;
            article.ModifiedByName = CreatedByName;
            article.UserId = 1;
            await _unitOfWork.Articles.AddAync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,$"{article.Title} başlıklı makale eklenmiştir.");
        }

        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await _unitOfWork.Articles.CountAsync();
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
            var categoriesCount = await _unitOfWork.Articles.CountAsync(c=>!c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Success, "hata", -1);
            }
        }

        public async Task<IResult> Delete(int articleID, string ModifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a=>a.Id==articleID);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a=>a.Id== articleID);
                article.ModifiedByName = ModifiedByName;
                article.ModifiedOn = DateTime.Now;
                article.IsDeleted = true;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale silinmiştir.");
            }
            return new Result(ResultStatus.Error,"makale bulunamadı.");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleID)
        {
            var article = await _unitOfWork.Articles.GetAsync(a=>a.Id == articleID,a=>a.User,a=>a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, "Böyle bir makale bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var article = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);
            if (article.Count >-1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = article,
                    ResultStatus=ResultStatus.Success
                }); 
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir makale bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryID)
        {
            var result =await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryID);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryID && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);
                if (article.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = article,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir makale bulunamadı", null);
            }
            
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var article = await _unitOfWork.Articles.GetAllAsync(a => a.IsDeleted == false, ar => ar.User, ar => ar.Category);
            if (article.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir makale bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedandActive()
        {
            var article = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted&&a.IsActive, ar => ar.User, ar => ar.Category);
            if (article.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir makale bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int articleID)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleID);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleID);
                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale silinmiştir.");
            }
            return new Result(ResultStatus.Error, "makale bulunamadı.");
        }

        public async Task<IResult> Update(ArticleUpdateDto category, string ModifiedByName)
        {
            var article = _mapper.Map<Article>(category);
            article.ModifiedByName = ModifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale güncellenmiştir.");
        }
    }
}
