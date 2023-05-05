using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Areas.Resource.Models
{
    public class Resource_6_Dto : BaseDto<Resource_6_Dto, Resource_6>
    {
        [Display(Name = "تعداد کارکنان اداری")]
        public int? MasterCount { get; set; }
        [Display(Name = "تعداد اعضای هیات علمی")]
        public int? ScienseCount { get; set; }
        [Display(Name = "در تاریخ")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
