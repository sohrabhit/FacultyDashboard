using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Notification : BaseEntity
    {
        //[Info(FieldType.Enum, typeof(Message_Type), "", "وضعیت پیام")]
        //public Message_Type? Message_Level { get; set; }

        [Info(FieldType.Strings, null, "", "پیام")]
        public string Message { get; set; }


        [Info(FieldType.Enum, typeof(Nofication_Type), "", "موقعیت پیام")]
        public Nofication_Type? Nofication_Type { get; set; }


        [Info(FieldType.Enum, typeof(NotificationStatus_Type), "", "موقعیت پیام")]
        public NotificationStatus_Type? NotificationStatus { get; set; }


        [Info(FieldType.DateTime, null, "", "تاریخ و زمان ثبت")]
        public DateTime? Date { get; set; } 

        [Info(FieldType.DateTime, null, "", "تاریخ و زمان")]
        [Column(TypeName = "datetime2")]
        public DateTime? Date_FA { get; set; }


        //[Info(FieldType.DateTime, null, "", "تاریخ و زمان شروع")]
        //public DateTime? StartDate { get; set; }
        //[Info(FieldType.DateTime, null, "", "تاریخ و زمان پایان")]
        //public DateTime? EndDate { get; set; }

        //[Info(FieldType.DateTime, null, "", "تاریخ و زمان شروع")]
        //[Column(TypeName = "datetime2")]
        //public DateTime? StartDate_FA { get; set; }
        //[Info(FieldType.DateTime, null, "", "تاریخ و زمان پایان")]
        //[Column(TypeName = "datetime2")]
        //public DateTime? EndDate_FA { get; set; }
    }
}
