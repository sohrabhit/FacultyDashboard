using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    // تعداد شرکت های دانش بنیان ایجاد شده توسط استادان و دانشجویان دانشکده ثبت اختراعات(کلی و به تفکیک گروه آموزشی)
    public class IT_2 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(ResourceGroupType), "", "گروه آموزشی")]
        public ResourceGroupType? Master { get; set; }
        [Info(FieldType.Integerr, null, "", "قرارداد کلی")]
        public int? Kol { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد")]
        public int? Count { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
