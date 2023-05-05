using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.IT.Models
{
    public class IT_7_Dto : BaseDto<IT_7_Dto, IT_7>
    {
        [Display(Name = "عنوان دوره")]
        public string Title { get; set; }
        [Display(Name = "گروه آموزشی")]
        public ResourceGroupType? Master { get; set; }
        //[Display(Name = "تعداد")]
        //public int? Count { get; set; }
        [Display(Name = "تاریخ برگزاری")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
