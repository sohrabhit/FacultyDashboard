using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Common.Utilities
{
    public class UtilityClass
    {
        public static string Convert_Persian_MothName(int mon)
        {
            string[] m = new string[] { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
            return m[mon - 1];
        }
        public static List<ChartWith3ColString> SortByMonth(List<ChartWith3ColString> amar_Report, string DateType, bool All_12_month,
            bool Merge_Month)
        {
            PersianCalendar pc = new PersianCalendar();
            List<ChartWith3ColString> data = new List<ChartWith3ColString>();
            if (DateType == "mon")
            {
                foreach (var item in amar_Report)
                {
                    var dd = DateAndTime.GetPersianDateTime(item.Date);
                    data.Add(new ChartWith3ColString()
                    {
                        Col1 = Convert_Persian_MothName(dd.Month),//item.Col1,
                        Col2 = item.Col2,
                        Col3 = dd.Month.ToString(),
                        Col4 = item.Col4,
                        Date = dd,
                        ChartType = "date",
                    });
                }
                if (All_12_month)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        if (!data.Any(x => x.Col3.Trim() == i.ToString()))
                        {
                            var dd = DateAndTime.GetPersianDateTime(new DateTime(2000, i, 1));
                            data.Add(new ChartWith3ColString()
                            {
                                Col1 = Convert_Persian_MothName(i),//item.Col1,
                                Col2 = "0",
                                Col3 = i.ToString(),
                                Col4 = "",
                                Date = dd,
                                ChartType = "date",
                            });
                        }
                    }
                }
                if (Merge_Month)
                {
                    data = data.GroupBy(x => x.Col3).Select((y, c) => new ChartWith3ColString()
                    {
                        Col1 = y.FirstOrDefault().Col1,
                        Col2 = y.Sum(k => int.Parse(k.Col2)).ToString(),
                        Col3 = y.FirstOrDefault().Col3,
                        Col4 = y.FirstOrDefault().Col4,
                        Date = y.FirstOrDefault().Date,
                        ChartType = y.FirstOrDefault().ChartType
                    }).ToList();
                }
            }
            else if (DateType == "year")
            {
                foreach (var item in amar_Report)
                {
                    var dd = DateAndTime.GetPersianDateTime(item.Date);
                    data.Add(new ChartWith3ColString()
                    {
                        Col1 = dd.Year.ToString(),//item.Col1,
                        Col2 = item.Col2,
                        Col3 = dd.Year.ToString(),
                        Col4 = item.Col4,
                        Date = dd,
                        ChartType = "date",
                    });
                }
                data = data.GroupBy(x => x.Col1).Select((y, c) => new ChartWith3ColString()
                {
                    Col1 = y.FirstOrDefault().Col1,
                    Col2 = y.Sum(k => int.Parse(k.Col2)).ToString(),
                    Col3 = y.FirstOrDefault().Col3,
                    Col4 = y.FirstOrDefault().Col4,
                    Date = y.FirstOrDefault().Date,
                    ChartType = y.FirstOrDefault().ChartType
                }).ToList();
            }
            else if (DateType == "day")
            {
                foreach (var item in amar_Report)
                {
                    var dd = DateAndTime.GetPersianDateTime(item.Date);
                    data.Add(new ChartWith3ColString()
                    {
                        Col1 = dd.ToString(),//item.Col1,
                        Col2 = item.Col2,
                        Col3 = dd.Day.ToString(),
                        Col4 = item.Col4,
                        Date = dd,
                        ChartType = "date",
                    });
                }
            }
            data = data.OrderByDescending(x => int.Parse(x.Col3)).ToList();
            return data;
        }
        public static List<ChartWith3ColString> SortByMonth_FA(List<ChartWith3ColString> amar_Report, string DateType, bool All_12_month,
            bool Merge_Month)
        {
            PersianCalendar pc = new PersianCalendar();
            List<ChartWith3ColString> data = new List<ChartWith3ColString>();
            if (DateType == "mon")
            {
                foreach (var item in amar_Report)
                {
                    var dd = DateAndTime.GetPersianDateTime(item.Date);
                    data.Add(new ChartWith3ColString()
                    {
                        Col1 = Convert_Persian_MothName(dd.Month),//item.Col1,
                        Col2 = item.Col2,
                        Col3 = dd.Month.ToString(),
                        Col4 = item.Col4,
                        Date = dd,
                        ChartType = "date",
                    });
                }
                if (All_12_month)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        if (!data.Any(x => x.Col3.Trim() == i.ToString()))
                        {
                            var dd = DateAndTime.GetPersianDateTime(new DateTime(2000, i, 1));
                            data.Add(new ChartWith3ColString()
                            {
                                Col1 = Convert_Persian_MothName(i),//item.Col1,
                                Col2 = "0",
                                Col3 = i.ToString(),
                                Col4 = "",
                                Date = dd,
                                ChartType = "date",
                            });
                        }
                    }
                }
                if (Merge_Month)
                {
                    data = data.GroupBy(x => x.Col3).Select((y, c) => new ChartWith3ColString()
                    {
                        Col1 = y.FirstOrDefault().Col1,
                        Col2 = y.Sum(k => int.Parse(k.Col2)).ToString(),
                        Col3 = y.FirstOrDefault().Col3,
                        Col4 = y.FirstOrDefault().Col4,
                        Date = y.FirstOrDefault().Date,
                        ChartType = y.FirstOrDefault().ChartType
                    }).ToList();
                }
            }
            else if (DateType == "year")
            {
                foreach (var item in amar_Report)
                {
                    var dd = DateAndTime.GetPersianDateTime(item.Date);
                    data.Add(new ChartWith3ColString()
                    {
                        Col1 = dd.Year.ToString(),//item.Col1,
                        Col2 = item.Col2,
                        Col3 = dd.Year.ToString(),
                        Col4 = item.Col4,
                        Date = dd,
                        ChartType = "date",
                    });
                }
                data = data.GroupBy(x => x.Col1).Select((y, c) => new ChartWith3ColString()
                {
                    Col1 = y.FirstOrDefault().Col1,
                    Col2 = y.Sum(k => int.Parse(k.Col2)).ToString(),
                    Col3 = y.FirstOrDefault().Col3,
                    Col4 = y.FirstOrDefault().Col4,
                    Date = y.FirstOrDefault().Date,
                    ChartType = y.FirstOrDefault().ChartType
                }).ToList();
            }
            else if (DateType == "day")
            {
                foreach (var item in amar_Report)
                {
                    var dd = DateAndTime.GetPersianDateTime(item.Date);
                    data.Add(new ChartWith3ColString()
                    {
                        Col1 = dd.ToString(),//item.Col1,
                        Col2 = item.Col2,
                        Col3 = dd.Day.ToString(),
                        Col4 = item.Col4,
                        Date = dd,
                        ChartType = "date",
                    });
                }
            }
            data = data.OrderByDescending(x => int.Parse(x.Col3)).ToList();
            return data;
        }
        public static string[,] ConvertToEnumMonth(List<ChartWith3ColString> data, Type enumtype/*, int enumitemcount*/)
        {
            PersianCalendar pc = new PersianCalendar();
            int enumitemcount = Enum.GetNames(enumtype).Length;
            string[,] amar_Report = new string[enumitemcount, 13];
            var test = "";
            for (int i = 0; i < enumitemcount; i++)
            {
                test = Enum.Parse(enumtype, i.ToString()).ToString();
                amar_Report[i, 0] = test.GetDisplayName(enumtype);
            }
            for (int i = 0; i < enumitemcount; i++)
            {
                test = Enum.Parse(enumtype, i.ToString()).ToString();
                for (int j = 1; j <= 12; j++)
                {
                    var item = data.Where(x => x.Col1 == test && x.Col3 == (j).ToString()).FirstOrDefault();
                    if (item != null)
                        amar_Report[i, j] = item.Col2;
                    else
                        amar_Report[i, j] = "";
                }
            }
            return amar_Report;
        }
        public static List<int> ConvertFirstTable(List<ChartWith3ColString> data, Type enumtype)
        {
            List<int> vals = new List<int>();
            int enumitemcount = Enum.GetNames(enumtype).Length;
            if (data != null && data.Count > 0)
            {
                for (int i = 0; i < enumitemcount; i++)
                {
                    var test = Enum.Parse(enumtype, i.ToString()).ToString();
                    var t = data.Any(x => x.Col1 == test);
                    if (t)
                        vals.Add(int.Parse(data.Where(x => x.Col1 == test).FirstOrDefault().Col2));
                    else
                        vals.Add(0);
                }
            }
            return vals;
        }
        public static List<float> ConvertToMonth(List<ChartWith3ColString> data, Type enumtype, bool Month_12/*, int enumitemcount*/)
        {
            PersianCalendar pc = new PersianCalendar();
            int enumitemcount = Enum.GetNames(enumtype).Length;
            //string[,] amar_Report = new string[enumitemcount, 13];
            List<float> amar_Report = new List<float>();
            if (Month_12)
            {
                var test = "";
                for (int i = 0; i < enumitemcount; i++)
                {
                    test = Enum.Parse(enumtype, i.ToString()).ToString();
                    //amar_Report[i, 0] = test.GetDisplayName(enumtype);
                }
                //for (int i = 0; i < enumitemcount; i++)
                //{
                //    test = Enum.Parse(enumtype, i.ToString()).ToString();
                    for (int j = 1; j <= 12; j++)
                    {
                        var item = data.Where(x => x.Col1 == (j).ToString()).FirstOrDefault();
                        if (item != null)
                            amar_Report.Add(float.Parse(item.Col2));
                        else
                            amar_Report.Add(0);
                    }
                //}
            }
            return amar_Report;
        }
        public static List<ChartWith3ColString> ConvertToMonth_Chart(List<ChartWith3ColString> data)
        {
            List<ChartWith3ColString> amar_Report = new List<ChartWith3ColString>();
            for (int i = 1; i <= 12; i++)
            {
                var item = data.Where(x => x.Col1 == i.ToString()).FirstOrDefault();
                if (item != null)
                {
                    amar_Report.Add(new ChartWith3ColString()
                    {
                        Col1 = i.ToString(),//UtilityClass.Convert_Persian_MothName(i),
                        Col2 = item.Col2
                    });
                }
                else
                {
                    amar_Report.Add(new ChartWith3ColString()
                    {
                        Col1 = i.ToString(),//UtilityClass.Convert_Persian_MothName(i),
                        Col2 = "0"
                    });
                }
            }
            amar_Report = amar_Report.OrderByDescending(x => int.Parse(x.Col1)).ToList();
            return amar_Report;
        }
        public static List<ChartWith3ColString> ConvertToMonth_Table(List<ChartWith3ColString> data)
        {
            List<ChartWith3ColString> table_amar_Report = new List<ChartWith3ColString>();
            for (int i = 1; i <= 12; i++)
            {
                var item = data.Where(x => x.Col1 == i.ToString()).FirstOrDefault();
                if (item != null)
                {
                    table_amar_Report.Add(new ChartWith3ColString()
                    {
                        Col1 = UtilityClass.Convert_Persian_MothName(i),
                        Col2 = item.Col2,
                        Col3 = i.ToString()
                    });
                }
                else
                {
                    table_amar_Report.Add(new ChartWith3ColString()
                    {
                        Col1 = UtilityClass.Convert_Persian_MothName(i),
                        Col2 = "0",
                        Col3 = i.ToString()
                    });
                }
            }
            table_amar_Report = table_amar_Report.OrderByDescending(x => int.Parse(x.Col3)).ToList();
            return table_amar_Report;
        }
    }
}
