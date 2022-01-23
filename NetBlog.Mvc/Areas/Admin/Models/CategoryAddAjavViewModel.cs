using NetBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Models
{
    public class CategoryAddAjavViewModel
    {
        public CategoryAddDto categoryAddDto { get; set; }
        public string CategoryAddPartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
