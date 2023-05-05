using Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // نسبت دانشجویان به اساتید 
    public class Education_6 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(MasterGroupType), "", "گروه آموزشی")]
        public MasterGroupType? MasterGroup { get; set; }
        //[Info(FieldType.Enum, typeof(GenderType), "", "جنسیت")]
        //public GenderType? Gender { get; set; }
        //[Info(FieldType.Integerr, null, "", "تعداد")]
        //public int? Count { get; set; }

        [Info(FieldType.Integerr, null, "", "تعداد اساتید")]
        public int? MasterCount { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد دانشجویان")]
        public int? StudentCount { get; set; }

        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}