using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Student.Models
{
    public class Student_1_Dto : BaseDto<Student_1_Dto, Student_1>
    {
        [Display(Name = "گروه آموزشی")]
        public MasterType? Master { get; set; }
        [Display(Name = "جنسیت")]
        public GenderType? Gender { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
