﻿using BLL.Interface;
using Domain.Entities;
using DTO.DataTable;
using DTO.User;
using Microsoft.EntityFrameworkCore;
using DTO.Base;
using Microsoft.AspNetCore.Http;
using Services.SessionServices;
using Utilities;
using LinqKit;
using Domain.Enums;
using Utilities.Extentions;

namespace BLL
{
    public class UserManager : Manager<User>, IUserManager
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly ISession Session;
        public UserManager(DbContext _Context, IHttpContextAccessor _httpContextAccessor) : base(_Context)
        {
            httpContextAccessor = _httpContextAccessor;
            Session = httpContextAccessor.HttpContext.Session;
        }



        /// <summary>
        /// گرفتن لیست کاربران برای نمایش در پنل مدیریت
        /// </summary>
        /// <returns></returns>
        public DataTableResponseDTO<UserDataTableDTO> GetDataTableDTO(DataTableSearchDTO searchData, UserFilterDataTableDTO filters)
        {
            return UOW.Users.GetDataTableDTO(searchData, filters);
        }





        /// <summary>
        /// گرفتن اطلاعات پایه کاربر با آیدی
        /// </summary>
        /// <param name="id">آیدی کاربر</param>
        /// <returns></returns>
        public UserBaseInfoDTO GetUserBaseInfo(long? id)
        {
            if (id == null) return null;
            return UOW.Users.GetUserBaseInfo((int)id);
        }




        /// <summary>
        /// تلفن همراه معتبر است؟
        /// </summary>
        /// <param name="Mobile">تلفن همراه</param>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public bool MobileIsUnique(string Mobile, long? Id = null)
        {
            return UOW.Users.MobileIsUnique(Mobile, Id);
        }




        /// <summary>
        /// نام کاربری معتبر است؟
        /// </summary>
        /// <param name="Username">نام کاربری</param>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public bool UsernameIsUnique(string Username, long? Id = null)
        {
            return UOW.Users.UsernameIsUnique(Username, Id);
        }





        /// <summary>
        /// ایجاد کاربر جدید
        /// </summary>
        /// <param name="model">مدل اطلاعات ادمین</param>
        /// <returns></returns>
        public BaseResult Create(UserCreateDTO model)
        {
            if (model.Password != model.RePassword)
                return new BaseResult { Status = false, Message = "تکرار کلمه عبور صحیح نمی باشد!" };

            var User = Session.GetUser();

            var user = new User()
            {
                Name = model.Name?.Trim().ToLower().ToPersianCharacter(),
                Username = model.Username?.Trim().ToLower().ToEnglishNumber(),
                RoleId = model.RoleId,
                Password = model.Password.GetHash(),
                Mobile = model.Mobile?.Trim().ToLower().ToEnglishNumber(),
                Type = model.Type,
                CreatorId = User?.Id
            };

            return base.Create(user);
        }






        /// <summary>
        /// گرفتن مدل لازم برای ویرایش کاربر
        /// </summary>
        /// <param name="id">آیدی کاربر</param>
        /// <returns></returns>
        public UserEditDTO GetEditDTO(long? id)
        {
            if (id == null) return null;
            return UOW.Users.GetOneDTO<UserEditDTO>(UserEditDTO.Selector, x => x.Id == id);
        }





        /// <summary>
        /// ویرایش کاربر
        /// </summary>
        /// <param name="model">اطلاعات کاربر</param>
        /// <returns></returns>
        public BaseResult Update(UserEditDTO model)
        {
            var User = UOW.Users.FirstOrDefault(x => x.Id == model.Id);
            User.Mobile = model.Mobile?.Trim().ToLower().ToEnglishNumber();
            User.Name = model.Name?.Trim().ToLower().ToPersianCharacter();
            User.Username = model.Username?.Trim().ToLower().ToEnglishNumber();
            User.IsEnabled = model.IsEnabled;
            User.Type = model.Type;
            User.RoleId = model.RoleId.Value;

            return base.Update(User);
        }






        /// <summary>
        /// ویرایش پروفایل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult UpdateProfile(UserEditProfileDTO model)
        {
            var User = UOW.Users.FirstOrDefault(x => x.Id == model.Id);
            User.Mobile = model.Mobile;
            User.Name = model.Name;
            User.Username = model.Username;

            return base.Update(User);

        }






        /// <summary>
        /// گرفتن مدل لازم برای ویرایش پروفایل توسط کاربر
        /// </summary>
        /// <param name="id">شناسه کاربر</param>
        /// <returns></returns>
        public UserEditProfileDTO GetEditProfileDTO(long? id)
        {
            if (id == null) return null;
            return UOW.Users.GetOneDTO<UserEditProfileDTO>(UserEditProfileDTO.Selector, x => x.Id == id);
        }





        /// <summary>
        /// تغییر کلمه عبور توسط ادمین
        /// </summary>
        /// <param name="model"> مدل تغییر کلمه عبور</param>
        /// <returns></returns>
        public BaseResult ChangePassword(UserChangePasswordDTO model)
        {
            var User = GetById(model.Id);
            if (User == null)
                return new BaseResult { Status = true, Message = "کاربر یافت نشد" };

            User.Password = model.Password.GetHash();

            var res = Update(User);
            if (res.Status)
                res.Message = "کلمه عبور با موفقیت تغییر یافت.";
            return res;
        }





        /// <summary>
        ///  گرفتن تعداد کل کاربران
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            return UOW.Users.Count();
        }






        /// <summary>
        ///  حذف کاربر به همراه حذف دسترسی های ثبت شده برای او
        /// </summary>
        /// <param name="id">آیدی کاربر</param>
        /// <returns></returns>
        public override bool Delete(object id)
        {
            try
            {
                UOW.Users.Remove(id);
                return UOW.Commit();
            }
            catch
            {
                return false;
            }
        }




        /// <summary>
        /// تغییر کلمه عبور توسط خود کاربر
        /// </summary>
        /// <param name="model">مدل تغییر کلمه عبور</param>
        /// <returns></returns>
        public BaseResult ProfileChangePassword(UserProfileChangePasswordDTO model)
        {
            var user = GetById(model.Id);

            if (!string.IsNullOrEmpty(model.OldPassword))
            {
                if (user.Password != model.OldPassword.GetHash())
                    return new BaseResult { Status = false, Message = "کلمه عبور فعلی صحیح نمی باشد." };
            }

            user.Password = model.Password.GetHash();
            user.PasswordIsChanged = true;
            var res = base.Update(user);
            if(res.Status)
            {
                var sessionUser = Session.GetUser();
                res.Model = sessionUser.PasswordIsChanged == false;
                sessionUser.PasswordIsChanged = true;
                Session.RemoveUser();
                Session.SetUser(sessionUser);
            }
            return res;
        }


      



        /// <summary>
        /// جستجوی کاربران
        /// </summary>
        /// <param name="word">بخشی از نام یا تلفن</param>
        /// <returns></returns>
        public List<UserSearchDTO> SearchUsers(string word)
        {
            var filter = PredicateBuilder.New<User>();

            filter.Start(x => x.IsEnabled);

            word = word.Trim().ToLower().ToEnglishNumber();
            if (!string.IsNullOrEmpty(word))
                filter.And(x => x.Name.Contains(word) || x.Username.Contains(word) || x.Mobile.Contains(word));
            var model = UOW.Users.GetDTO<UserSearchDTO>(UserSearchDTO.Selector, filter, null, null, 15).ToList();
            return model;
        }




        /// <summary>
        /// جستجوی کاربران یک فروشگاه 
        /// </summary>
        /// <param name="StoreId">شناسه فروشگاه</param>
        /// <returns></returns>
        public List<UserSearchDTO> GetUsersForFactors(UserType? Type)
        {
            var filter = PredicateBuilder.New<User>();
            filter.Start(x => x.IsEnabled);

            if (Type != null)
                filter.And(x => x.Type == Type);

            var model = UOW.Users.GetDTO<UserSearchDTO>(UserSearchDTO.Selector, filter).ToList();
            return model;
        }



        /// <summary>
        /// جستجو کاربر برای سلکتایز
        /// </summary>
        /// <param name="word">متن جستجو</param>
        public List<SelectListDTO> Search(string word)
        {
            #region شرطها
            var filter = PredicateBuilder.New<User>(true);

            if (!string.IsNullOrEmpty(word))
            {
                word = word.Trim().ToLower().ToEnglishNumber();
                filter.And(x => x.Name.Contains(word) || x.Username.Contains(word) || x.Mobile.Contains(word));
            }
            #endregion

            return UOW.Users.GetDTO<SelectListDTO>(SelectListDTO.UserSelector, filter, take: 20).ToList();
        }



    }
}
