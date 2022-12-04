﻿using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace DTO.User
{
    /// <summary>
    /// ویرایش پرسنل
    /// </summary>
    public class UserEditDTO
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }



        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} الزامی است.")]
        [MaxLength(50, ErrorMessage = "{0} حداکثر می تواند {1} کاراکتر باشد.")]
        public string Name { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} الزامی است.")]
        [StringLength(30, ErrorMessage = "{0} حداکثر {1} کاراکتر باشد.")]
        [Remote("UsernameIsUnique", "CheckUnique", "Global", AdditionalFields = "Id", HttpMethod = "post", ErrorMessage = "نام کاربری قبلا استفاده شده است!")]
        public string Username { get; set; }



        [Display(Name = "تلفن")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} باید {1} کاراکتر باشد.")]
        [RegularExpression(@"^0\d{10}", ErrorMessage = "تلفن با صفر شروع شود و حاوی عدد به طول 11 کاراکتر باشد.")]
        [Remote("MobileIsUnique", "CheckUnique", "Global", AdditionalFields = "Id", HttpMethod = "post", ErrorMessage = "تلفن قبلا استفاده شده است!")]
        public string Mobile { get; set; }


        [Display(Name = "دسترسی کاربر")]
        [Required(ErrorMessage = "{0} الزامی است.")]
        public long? RoleId { get; set; }


        [Display(Name = "نوع کاربر")]
        [Required(ErrorMessage = "{0} الزامی است.")]
        public UserType Type { get; set; }


        [Display(Name = "فعال است")]
        public bool IsEnabled { get; set; }


        /// <summary>
        /// بروزرسانی نقش ها
        /// </summary>
        public bool ApplyNewAccess { get; set; }


        public UserEditDTO()
        {
            ApplyNewAccess = false;
        }


        /// <summary>
        /// فیلدهای لازم برای استخراج مدل از موجودیت مربوطه
        /// </summary>
        public static Expression<Func<Domain.Entities.User, UserEditDTO>> Selector
        {
            get
            {
                return model => new UserEditDTO()
                {
                    Id = model.Id,
                    Mobile = model.Mobile,
                    Name = model.Name ,
                    RoleId = model.RoleId,
                    IsEnabled = model.IsEnabled,
                    Username = model.Username,
                    Type = model.Type
                };
            }
        }


    }
}
