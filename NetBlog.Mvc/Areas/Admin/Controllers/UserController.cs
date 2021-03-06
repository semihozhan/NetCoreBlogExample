using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBlog.Entities.Concreate;
using NetBlog.Entities.Dtos;
using NetBlog.Mvc.Areas.Admin.Models;
using NetBlog.Mvc.Helpers.Abstract;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public UserController(UserManager<User> userManager,IMapper mapper, SignInManager<User> signInManager, IImageHelper imageHelper)
        {
            _userManager=userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }
       [Authorize(Roles ="Admin")]
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
        public IActionResult Login()
        {
            return View("UserLogin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eposta veya şifre hatalı");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eposta veya şifre hatalı");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
            
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home", new {
                Area=""
            });
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public  IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var UploadedUserImageDtoResult = await _imageHelper.UploadedUserImage(userAddDto.UserName, userAddDto.PictureFile);
                userAddDto.Picture = UploadedUserImageDtoResult.ResultStatus==ResultStatus.Success? UploadedUserImageDtoResult.Data.FullName : "userImages/defaultUser.png";
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


        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Id==userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile!=null)
                {
                    var UploadedUserImageDtoResult = await _imageHelper.UploadedUserImage(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = UploadedUserImageDtoResult.ResultStatus == ResultStatus.Success ? UploadedUserImageDtoResult.Data.FullName : "userImages/defaultUser.png";
                    isNewPictureUploaded = true;
                }
                var updatedUser = _mapper.Map<UserUpdateDto,User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.Detele(oldUserPicture);
                    }
                    var userUpdateViewmodel = JsonSerializer.Serialize(new UserUpdateAjavViewModel
                    {
                        userDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = "Güncellendi",
                            user = updatedUser
                        },
                        userUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewmodel);
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    var userUpdateErrorViewmodel = JsonSerializer.Serialize(new UserUpdateAjavViewModel
                    {
                        userUpdateDto = userUpdateDto,
                        userUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateErrorViewmodel);
                }
            }
            else
            {
                var userUpdateErrorModelStateViewmodel = JsonSerializer.Serialize(new UserUpdateAjavViewModel
                {
                    userUpdateDto = userUpdateDto,
                    userUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateErrorModelStateViewmodel);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var UploadedUserImageDtoResult = await _imageHelper.UploadedUserImage(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = UploadedUserImageDtoResult.ResultStatus == ResultStatus.Success ? UploadedUserImageDtoResult.Data.FullName : "userImages/defaultUser.png";
                    if (oldUserPicture!= "userImages/defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }
                    
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.Detele(oldUserPicture);
                    }
                    TempData.Add("successMessage", "güncellendi");
                    return View(userUpdateDto);
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    
                    return View(userUpdateDto);
                }
            }
            else
            {
                return View(userUpdateDto);
            }
        }

        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if(isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword, userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword,true,false);
                        TempData.Add("successMessage", "güncellendi");
                        return View();
                    }
                    ModelState.AddModelError("", "Hata ile karşılaşıldı");
                    return View(userPasswordChangeDto);
                }
                ModelState.AddModelError("","Hata ile karşılaşıldı");
                return View(userPasswordChangeDto);
            }
            ModelState.AddModelError("", "Hata ile karşılaşıldı");
            return View(userPasswordChangeDto);
        }

        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View("AccessDenied");
        }

 
      

       
    }
}
