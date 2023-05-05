using Entities;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Models
{
    public class UserReportDto : BaseDto<UserReportDto, UserReport/*, int*/>
    {
        [Display(Name = "نام نمودار")]
        public string Indicator_Name { get; set; }

        //[Display(Name = "نوع نمودار")]
        //public Chart_Type ChartType { get; set; }

        //[Display(Name = "کاربر ثبت کننده")]
        //public User RegistereUser { get; set; }
    }
}
