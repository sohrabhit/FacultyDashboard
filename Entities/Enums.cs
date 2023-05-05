using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public enum Nofication_Type : byte
    {
        [Display(Name = "بیماران تعیین تکلیف شده ظرف مدت 6ساعت")]
        Quality10,
        [Display(Name = "بیماران تعیین تکلیف شده ظرف مدت 12ساعت")]
        Quality21,
        [Display(Name = "بیماران ترخص شده از بخش اورژانس براساس نوع ترخیص")]
        Activity_1,
        [Display(Name = "درصدCPR موفق بیماران ترومایی")]
        Quality_4,
        [Display(Name = "اطلاعات درصدCPR موفق بیماران داخلی")]
        Quality_5,
        [Display(Name = "درصدCPR موفق بیماران فاقد علائم حیاتی")]
        Quality_6,
        [Display(Name = "میزان مرگ و میر بخش اورژانس در بیماران دارای علائم حیاتی")]
        Quality_1,
        [Display(Name = "متوسط زمان تریاژ تا اولین ویزیت پزشکی به ازا سطح 1 تریاژ")]
        Quality_15_1,
        [Display(Name = "متوسط زمان تریاژ تا اولین ویزیت پزشکی به ازا سطح 2 تریاژ")]
        Quality_15_2,
        [Display(Name = "متوسط زمان تریاژ تا اولین ویزیت پزشکی به ازا سطح 3 تریاژ")]
        Quality_15_3,
        [Display(Name = "متوسط زمان تریاژ تا اولین ویزیت پزشکی به ازا سطح 4 تریاژ")]
        Quality_15_4,
        [Display(Name = "متوسط زمان تریاژ تا اولین ویزیت پزشکی به ازا سطح 5 تریاژ")]
        Quality_15_5,
        [Display(Name = "میانگین مدت زمان درخواست آزمایش تا زمان تحویل جواب آزمایش سطح 1")]
        Quality_19_1,
        [Display(Name = "میانگین مدت زمان درخواست آزمایش تا زمان تحویل جواب آزمایش سطح 2")]
        Quality_19_2,
        [Display(Name = "میانگین مدت زمان درخواست آزمایش تا زمان تحویل جواب آزمایش سطح 3")]
        Quality_19_3,
        [Display(Name = "میانگین مدت زمان درخواست آزمایش تا زمان تحویل جواب آزمایش سطح 4")]
        Quality_19_4,
        [Display(Name = "میانگین مدت زمان درخواست آزمایش تا زمان تحویل جواب آزمایش سطح 5")]
        Quality_19_5,
        [Display(Name = "میانگین مدت زمان فاصله انجام راديوگرافي تا حاضرشدن كليشه راديولوژي سطح 1")]
        Quality_20_1,
        [Display(Name = "میانگین مدت زمان فاصله انجام راديوگرافي تا حاضرشدن كليشه راديولوژي سطح 2")]
        Quality_20_2,
        [Display(Name = "میانگین مدت زمان فاصله انجام راديوگرافي تا حاضرشدن كليشه راديولوژي سطح 3")]
        Quality_20_3,
        [Display(Name = "میانگین مدت زمان فاصله انجام راديوگرافي تا حاضرشدن كليشه راديولوژي سطح 4")]
        Quality_20_4,
        [Display(Name = "میانگین مدت زمان فاصله انجام راديوگرافي تا حاضرشدن كليشه راديولوژي سطح 5")]
        Quality_20_5
    }
    public enum NotificationStatus_Type : byte
    {
        [Display(Name = "مطلوب")]
        ok,
        [Display(Name = "متوسط")]
        midle,
        [Display(Name = "نامطلوب")]
        no
    }
    public enum Message_Type : byte
    {
        [Display(Name = "خوانده شده")]
        Read,
        [Display(Name = "خوانده نشده")]
        UnRead
    }
    public enum Chart_Type : byte
    {
        [Display(Name = "میله ای")]
        M1,
        //[Display(Name = "دایره ای")]
        //M2,
        [Display(Name = "جدول")]
        M3
    }
  
    public enum DateGroupBy_Type : byte
    {
        [Display(Name = "سال")]
        Year,
        [Display(Name = "ماه")]
        Month,
        [Display(Name = "روز")]
        Day
    }
    
    public enum Attribute_Type : byte
    {
        INT,
        Bool,
        DateTime,
        Float,
        //Avg
    }

    public enum Report_ComputType : byte
    {
        Sum,
        Percent,
        Avg,
        Count,

        Custome
    }
    public enum Report_FieldType : byte
    {
        INT,
        Percent,
        MultiSelect,
        Bool,
        DateTime,
        Float,
        //Avg
    }
    public enum LanguageType //: byte
    {
        FA,
        EN
        //Avg
    }
    public enum Report_Type //: byte
    {
        Sum,
        Avg,
        Percent
        //Avg
    }
    public enum GenderType : byte
    {
        [Display(Name = "مرد")]
        Male,
        [Display(Name = "زن")]
        Female
    }
    public enum MarialType : byte
    {
        [Display(Name = "مجرد")]
        L1,
        [Display(Name = "متاهل")]
        L2
    }
    public enum EducationType : byte
    {
        [Display(Name = "کاردانی")]
        L1,
        [Display(Name = "کارشناسی")]
        L2,
        [Display(Name = "کارشناسی ارشد")]
        L3,
        [Display(Name = "دکترای تخصصی")]
        L4
    }
    public enum AvgEducationType : byte
    {
        [Display(Name = "کارشناسی")]
        L1,
        [Display(Name = "کارشناسی ارشد")]
        L2,
        [Display(Name = "دکترای تخصصی")]
        L3
    }
    public enum MasterGroupType : byte
    {
        [Display(Name = "فوریت پزشکی")]
        L1,
        [Display(Name = "فوریت پزشکی پیش بیمارستانی دریا")]
        L2,
        [Display(Name = "گروه علوم آزمایشگاهی")]
        L3,
        [Display(Name = "گروه رادیولوژی")]
        L4,
        [Display(Name = "گروه هوشبری")]
        L5,
        [Display(Name = "گروه فناوری اطلاعات سلامت")]
        L6,
        [Display(Name = "گروه اتاق عمل")]
        L7,
        [Display(Name = "گروه فوریتهای پزشکی")]
        L8,
        [Display(Name = "گروه زبان")]
        L9
    }
    public enum ResourceGroupType : byte
    {
        [Display(Name = "گروه علوم آزمایشگاهی")]
        L1,
        [Display(Name = "گروه رادیولوژی")]
        L2,
        [Display(Name = "گروه هوشبری")]
        L3,
        [Display(Name = "گروه فناوری اطلاعات سلامت")]
        L4,
        [Display(Name = "گروه اتاق عمل")]
        L5,
        [Display(Name = "گروه فوریتهای پزشکی")]
        L6,
        [Display(Name = "گروه زبان")]
        L7
    }
    public enum ResourceType : byte
    {
        [Display(Name = "pubmed")]
        L1,
        [Display(Name = "scopus")]
        L2,
        [Display(Name = "ISI")]
        L3,
        [Display(Name = "علمی - پژوهشی")]
        L4,
        [Display(Name = "علمی - ترویجی")]
        L5,
        [Display(Name = "کنفرانس های داخلی")]
        L6,
        [Display(Name = "کنفرانس های بین المللی")]
        L7
    }
    public enum CeminarType : byte
    {
        [Display(Name = "داخلی")]
        L1,
        [Display(Name = "بین المللی")]
        L2
    }
    public enum TermType : byte
    {
        [Display(Name = "نیمسال اول")]
        L1,
        [Display(Name = "نیمسال دوم")]
        L2
    }
    public enum SienceGroupType : byte
    {
        [Display(Name = "مربی")]
        L1,
        [Display(Name = "استادیار")]
        L2,
        [Display(Name = "دانشیار")]
        L3,
        [Display(Name = "استاد")]
        L4
    }
    public enum MasterType : byte
    {
        [Display(Name = "علوم آزمایشگاهی")]
        L1,
        [Display(Name = "هوشبری")]
        L2,
        [Display(Name = "اتاق عمل")]
        L3,
        [Display(Name = "رادیولوژی")]
        L4,
        [Display(Name = "فوریت پزشکی")]
        L5,
        [Display(Name = "هوشبری")]
        L6,
        [Display(Name = "اتاق عمل")]
        L7,
        [Display(Name = "رادیولوژی")]
        L8,
        [Display(Name = "فوریت پزشکی")]
        L9
    }
    public enum CeremonyType : byte
    {
        [Display(Name = "مناسبتی(تقویمی)")]
        L1,
        [Display(Name = "مناسبتی(شغلی)")]
        L2
    }
    public enum FacultyType : byte
    {
        [Display(Name = "رسمی")]
        L1,
        [Display(Name = "پیمانی")]
        L2,
        [Display(Name = "مدعو")]
        L3
    }


    public enum Have_Type : byte
    {
        [Display(Name = "دارد")]
        M1,
        [Display(Name = "ندارد")]
        M2,
        [Display(Name = "نمی داند")]
        M3
    }
    public enum EN_Have_Type
    {
        [Display(Name = "Have")]
        M1,
        [Display(Name = "Dont Have")]
        M2,
        [Display(Name = "Not know")]
        M3
    }
    public enum YesNo_Type
    {
        [Display(Name = "بله")]
        M1,
        [Display(Name = "خیر")]
        M2
    }
    public enum EN_YesNo_Type
    {
        [Display(Name = "Yes")]
        M1,
        [Display(Name = "No")]
        M2
    }
}
