using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Resource.Models
{
    public class Resource_7_Dto : BaseDto<Resource_7_Dto, Resource_7>
    {
        [Display(Name = "نوع هیئت علمی")]
        public SienceGroupType? SienceGroup { get; set; }
        [Display(Name = "جنسیت")]
        public GenderType? Gender { get; set; }
        [Display(Name = "تعداد اعضا")]
        public int? ScienseCount { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
