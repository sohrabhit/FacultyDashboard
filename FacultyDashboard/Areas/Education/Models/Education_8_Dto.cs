using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Education.Models
{
    public class Education_8_Dto : BaseDto<Education_8_Dto, Education_8>
    {
        [Display(Name = "مقطع تحصیلی")]
        public EducationType? EducationDegree { get; set; }
        [Display(Name = "جنسیت")]
        public GenderType? Gender { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }

        [Display(Name = "در تاریخ")]
        public DateTime? RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
