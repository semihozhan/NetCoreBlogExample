﻿using NetBlog.Entities.Concreate;
using NetBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Dtos
{
    public class UserDto : DtoGetBase
    {
        public User user { get; set; }
    }
}
