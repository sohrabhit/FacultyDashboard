using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserReport : BaseEntity
    {
        [Info(FieldType.Strings, null, "", "نام نمودار")]
        public string Indicator_Name { get; set; }

        [Info(FieldType.Enum, typeof(Chart_Type), "", "نوع نمودار")]
        public Chart_Type ChartType { get; set; }

        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
