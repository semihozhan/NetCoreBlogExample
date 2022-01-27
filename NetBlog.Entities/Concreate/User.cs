﻿using Microsoft.AspNetCore.Identity;
using NetBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Concreate
{
    public class User:IdentityUser<int>
    {
        
        public string Picture { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
