using AutoMapper;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Services.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>().ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
