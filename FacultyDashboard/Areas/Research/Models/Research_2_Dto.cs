using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Research.Models
{
    public class Research_2_Dto : BaseDto<Research_2_Dto, Research_2>
    {
        [Display(Name = "نوع سمینار")]
        public CeminarType? Ceminar { get; set; }
        [Display(Name = "نیمسال تحصیلی")]
        public TermType? Term { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
