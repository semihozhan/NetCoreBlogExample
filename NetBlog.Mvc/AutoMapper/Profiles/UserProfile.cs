using AutoMapper;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User,UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
