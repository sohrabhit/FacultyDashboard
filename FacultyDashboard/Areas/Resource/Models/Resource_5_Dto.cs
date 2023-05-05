using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Resource.Models
{
    public class Resource_5_Dto : BaseDto<Resource_5_Dto, Resource_5>
    {
        [Display(Name = "تعداد کارکنان اداری")]
        public int? MasterCount { get; set; }
        [Display(Name = "تعداد دانشجویان")]
        public int? StudentCount { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
