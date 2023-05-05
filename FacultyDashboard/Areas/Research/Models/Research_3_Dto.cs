using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Research.Models
{
    public class Research_3_Dto : BaseDto<Research_3_Dto, Research_3>
    {
        [Display(Name = "گروه آموزشی")]
        public ResourceGroupType? ResourceGroup { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
