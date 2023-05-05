﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    // کارگاه های مهارت آموزی( مختص کارکنانی که دریافت امتیاز آموزش مداون برای آنها مطرح نیست
    public class IT_6 : BaseEntity
    {
        [Info(FieldType.Strings, null, "", "عنوان کارگاه")]
        public string Title { get; set; }
        [Info(FieldType.Enum, typeof(ResourceGroupType), "", "گروه آموزشی")]
        public ResourceGroupType? Master { get; set; }
        //[Info(FieldType.Integerr, null, "", "تعداد")]
        //public int? Count { get; set; }
        [Info(FieldType.DateTime, null, "", "تاریخ برگزاری")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
