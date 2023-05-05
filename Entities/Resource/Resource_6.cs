﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    // نسبت كاركنان اداري به اعضاي هيأت علمي
    public class Resource_6 : BaseEntity
    {
        [Info(FieldType.Integerr, null, "", "تعداد کارکنان اداری")]
        public int? MasterCount { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد اعضای هیات علمی")]
        public int? ScienseCount { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
