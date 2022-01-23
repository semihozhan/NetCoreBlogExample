using Microsoft.AspNetCore.Mvc;
using NetBlog.Entities.Dtos;
using NetBlog.Mvc.Areas.Admin.Models;
using NetBlog.Services.Abstract;
using NetBlog.Shared.Utilities.Extensions;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_categoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto,"Semih Özhan");
                if (result.ResultStatus==ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjavViewModel { 
                        CategoryDto=result.Data,
                        CategoryAddPartial=await this.RenderViewToStringAsync("_categoryAddPartial", categoryAddDto),

                    });
                    return Json(categoryAddAjaxModel);
                }
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjavViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_categoryAddPartial", categoryAddDto),

            });
            return Json(categoryAddAjaxErrorModel);
        }
    }
}
