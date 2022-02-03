using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }

        public int ArticlesCount { get; set; }

        public int CommentsCount { get; set; }

        public int UsersCount { get; set; }

        public ArticleListDto Articles { get; set; }
    }
}
