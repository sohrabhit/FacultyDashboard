using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    // تعداد مقاله های نمایه شده در مجلات) pubmed ،scopus ،ISI، علمی - پژوهشی، علمی - ترویجی، کنفرانس های داخلی و بین المللی((کلی و به تفکیک گروه آموزشی)
    public class Research_1 : BaseEntity
    {
        [Info(FieldType.Enum, typeof(ResourceGroupType), "", "گروه آموزشی")]
        public ResourceGroupType? ResourceGroup { get; set; }
        [Info(FieldType.Enum, typeof(ResourceType), "", "نوع مقاله")]
        public ResourceType? Resource_Type { get; set; }
        [Info(FieldType.Integerr, null, "", "تعداد")]
        public int? Count { get; set; }
        [Info(FieldType.DateTime, null, "", "در تاریخ")]
        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
        [Info(FieldType.Class, null, "User", "کاربر ثبت کننده")]
        public User RegistereUser { get; set; }
    }
}
