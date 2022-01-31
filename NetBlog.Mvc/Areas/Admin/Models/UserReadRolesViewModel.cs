using Microsoft.AspNetCore.Mvc;
using NetBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Models
{
    public class UserReadRolesViewModel 
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
