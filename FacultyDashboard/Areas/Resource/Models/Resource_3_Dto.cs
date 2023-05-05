using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Resource.Models
{
    public class Resource_3_Dto : BaseDto<Resource_3_Dto, Resource_3>
    {
        [Display(Name = "گروه آموزشی")]
        public ResourceGroupType? Master { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
