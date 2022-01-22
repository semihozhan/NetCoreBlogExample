using AutoMapper;
using NetBlog.Entities.Concreate;
using NetBlog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>().ForMember(dest=>dest.CreatedOn,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest=>dest.ModifiedOn,opt=>opt.MapFrom(x=>DateTime.Now));
        }
    }
}
