using Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // ميانگين معدل سالانه دانشجويان
    public class Education_10 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(AvgEducationType), "", "مقطع تحصیلی")]
        public AvgEducationType? AvgEducationDegree { get; set; }
        [Info(FieldType.Enum, typeof(GenderType), "", "جنسیت")]
        public GenderType? Gender { get; set; }
        [Info(FieldType.Float, null, "", "معدل")]
        public float? Avg { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد")]
        public int? Count { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}