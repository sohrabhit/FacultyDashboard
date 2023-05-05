using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Setting : BaseEntity
    {
        //public string Level_name { get; set; }
        // Resource_7
        public int? Resource_7_All { get; set; } = 0; // تعداد اعضاء هیئت علمی 
        public int? Resource_7_1 { get; set; } = 0; // مربی
        public int? Resource_7_2 { get; set; } = 0; // استادیار
        public int? Resource_7_3 { get; set; } = 0; // دانشیار
        public int? Resource_7_4 { get; set; } = 0; // استاد
        // Research_1// تعداد مقالات 
        public int? Research_Count { get; set; } 
        // Education_1
        public int? Education_1_All { get; set; } = 0; // تعدادکل  دانشجویان 
        public int? Education_1_1 { get; set; } = 0; // کاردانی
        public int? Education_1_2 { get; set; } = 0; // کارشناسی
        public int? Education_1_3 { get; set; } = 0; // ارشد
        public int? Education_1_4 { get; set; } = 0; // دکترا
        // Education_2
        public int? Education_2_All { get; set; } = 0; // دانشجویان ورودی 
        public int? Education_2_1 { get; set; } = 0; // کاردانی
        public int? Education_2_2 { get; set; } = 0; // کارشناسی
        public int? Education_2_3 { get; set; } = 0; // ارشد
        public int? Education_2_4 { get; set; } = 0; // دکترا
        //public int? State_5 { get; set; }
    }
}
