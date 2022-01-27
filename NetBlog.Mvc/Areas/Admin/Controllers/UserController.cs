using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager=userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }
    }
}
