﻿using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.IT.Models
{
    public class IT_4_Dto : BaseDto<IT_4_Dto, IT_4>
    {
        [Display(Name = "گروه آموزشی")]
        public ResourceGroupType? Master { get; set; }
        [Display(Name = "قرارداد کلی")]
        public int? Kol { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
