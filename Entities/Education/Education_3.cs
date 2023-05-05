﻿using Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // آمار دانشجویان بورسیه ( سپاه،ناجا،وزارت دفاع)
    public class Education_3 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(EducationType), "", "مقطع تحصیلی")]
        public EducationType? EducationDegree { get; set; }
        [Info(FieldType.Enum, typeof(GenderType), "", "جنسیت")]
        public GenderType? Gender { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد")]
        public int? Count { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}