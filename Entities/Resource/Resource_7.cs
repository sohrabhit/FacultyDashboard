﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    // تعداد هیأت علمی به مرتبه علمی و جنسیت
    public class Resource_7 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(SienceGroupType), "", "نوع هیئت علمی")]
        public SienceGroupType? SienceGroup { get; set; }
        [Info(FieldType.Enum, typeof(GenderType), "", "جنسیت")]
        public GenderType? Gender { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد اعضا")]
        public int? ScienseCount { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
