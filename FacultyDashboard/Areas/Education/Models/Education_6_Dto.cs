using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Education.Models
{
    public class Education_6_Dto : BaseDto<Education_6_Dto, Education_6>
    {
        [Display(Name = "گروه آموزشی")]
        public MasterGroupType? MasterGroup { get; set; }
        [Display(Name = "جنسیت")]
        public GenderType? Gender { get; set; }
        //[Display(Name = "تعداد")]
        //public int? Count { get; set; }
        [Display(Name = "تعداد اساتید")]
        public int? MasterCount { get; set; }
        [Display(Name = "تعداد دانشجویان")]
        public int? StudentCount { get; set; }

        [Display(Name = "در تاریخ")]
        public DateTime? RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
