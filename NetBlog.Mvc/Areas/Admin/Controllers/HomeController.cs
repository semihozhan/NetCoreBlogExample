using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBlog.Entities.Concreate;
using NetBlog.Mvc.Areas.Admin.Models;
using NetBlog.Services.Abstract;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userService;

        public HomeController(ICategoryService categoryService, IArticleService articleService, ICommentService commentService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _commentService = commentService;
            _userService = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountByIsDeleted();
            var articlesCountResult = await _articleService.CountByIsDeleted();
            var commentsCountResult = await _commentService.CountByIsDeleted();
            var usersCountResult = await _userService.Users.CountAsync();

            var articleResult = await _articleService.GetAll();

            if (categoriesCountResult.ResultStatus==ResultStatus.Success&& articlesCountResult.ResultStatus == ResultStatus.Success && commentsCountResult.ResultStatus == ResultStatus.Success && usersCountResult>-1 && articleResult.ResultStatus == ResultStatus.Success )
            {
                return View(new DashboardViewModel
                {
                    ArticlesCount= articlesCountResult.Data,
                    CommentsCount= commentsCountResult.Data,
                    UsersCount= usersCountResult,
                    CategoriesCount= categoriesCountResult.Data,
                    Articles=articleResult.Data
                });
            }

            return NotFound();
        }
    }
}
