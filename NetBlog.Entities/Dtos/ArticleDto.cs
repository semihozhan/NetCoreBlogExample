﻿using NetBlog.Entities.Concreate;
using NetBlog.Shared.Entities.Abstract;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Dtos
{
    public  class ArticleDto : DtoGetBase
    {
        public Article Article { get; set; }

    }
}
