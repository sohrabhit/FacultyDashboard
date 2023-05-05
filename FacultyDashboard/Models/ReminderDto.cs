using Entities;
using System;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FacultyDashboard.Models
{
    public class ReminderDto : BaseDto<ReminderDto, Reminder/*, int*/>
    { //[Info(FieldType.Enum, typeof(Message_Type), "", "وضعیت پیام")]
        //public Message_Type? Message_Level { get; set; }

        [Display(Name = "عنوان پیام")]
        public string Title { get; set; }

        [Display(Name = "پیام")]
        public string Message { get; set; }


        [Display(Name = "وضعیت پیام")]
        public Message_Type? Message_Type { get; set; }


        [Display(Name = "تاریخ و زمان یادآوری")]
        public DateTime? RegisterDate { get; set; }

        //[Display(Name = "تاریخ و زمان")]
        //public DateTime? Date_FA { get; set; }

        [Display(Name = "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}