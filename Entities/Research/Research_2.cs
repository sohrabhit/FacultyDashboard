using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    // تعداد سمینارهای علمی داخلی و بین المللی برگزار شده در دانشکده
    public class Research_2 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(CeminarType), "", "نوع سمینار")]
        public CeminarType? Ceminar { get; set; }
        [Info(FieldType.Enum, typeof(TermType), "", "نیسال تحصیلی")]
        public TermType? Term { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد")]
        public int? Count { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
