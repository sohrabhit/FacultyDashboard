using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Reminder : BaseEntity
    {
        //[Info(FieldType.Enum, typeof(Message_Type), "", "وضعیت پیام")]
        //public Message_Type? Message_Level { get; set; }

        [Info(FieldType.Strings, null, "", "عنوان پیام")]
        public string Title { get; set; }

        [Info(FieldType.Strings, null, "", "پیام")]
        public string Message { get; set; }


        [Info(FieldType.Enum, typeof(Message_Type), "", "وضعیت پیام")]
        public Message_Type? Message_Type { get; set; }


        [Info(FieldType.DateTime, null, "", "تاریخ و زمان یادآوری")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }

        //[Info(FieldType.DateTime, null, "", "تاریخ و زمان")]
        //[Column(TypeName = "datetime2")]
        //public DateTime? Date_FA { get; set; }

        //public DateTime? CreateDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}