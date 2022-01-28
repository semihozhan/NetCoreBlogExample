using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using NetBlog.Mvc.Areas.Admin.Models;
using NetBlog.Shared.Utilities.Extensions;
using NetBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager,IMapper mapper)
        {
            _userManager=userManager;
            _mapper = mapper;
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
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto= JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            },new JsonSerializerOptions {
                ReferenceHandler=ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        [HttpGet]
        public  IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        public async Task<JsonResult> Delete(int userID)
        {
            var user = await _userManager.FindByIdAsync(userID.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUSer = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus= ResultStatus.Success,
                    Message="silindi",
                    user = user
                });
                return Json(deletedUSer);
            }
            else
            {
                string errorMesages = String.Empty;
                foreach (var err in result.Errors)
                {
                    errorMesages = $"*{err.Description}\n";
                }
                var userdeletedErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = errorMesages,
                    user = user
                });
                return Json(userdeletedErrorModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto);
                var user= _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjavViewModel
                    {
                        userDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = "Başarı ile eklenmiştir.",
                            user = user
                        },
                        userAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto),

                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("",err.Description);
                    }
                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjavViewModel
                    {
                        userAddDto = userAddDto,
                        userAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto),

                    });
                    return Json(userAddAjaxErrorModel);
                }
            }
            var modelStateErrorModel = JsonSerializer.Serialize(new UserAddAjavViewModel
            {
                userAddDto = userAddDto,
                userAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto),

            });
            return Json(modelStateErrorModel);
        }

        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            
            //if (string.IsNullOrWhiteSpace(_env.WebRootPath))
            //{
            //    _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            //}
            string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.Picture.FileName);
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string fileName = $"{userAddDto.UserName}_{DateTimeExtensions.FullDateandTimeStringwithUnderscore(dateTime)}{fileExtension}";

            var path = Path.Combine($"{wwwroot}/img",fileName);

            await using (var stream = new FileStream(path,FileMode.Create))
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
