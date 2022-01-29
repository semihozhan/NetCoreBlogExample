using NetBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Models
{
    public class UserUpdateAjavViewModel
    {
        public UserUpdateDto userUpdateDto { get; set; }
        public string userUpdatePartial { get; set; }
        public UserDto userDto { get; set; }
    }
}
