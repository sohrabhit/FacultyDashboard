using Persia;
using System;

namespace Common.Utilities
{
    public static class DateAndTime
    {
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }



        public static DateTime? revercedatenull(object obj)
        {
            if (obj == null || obj.ToString() == "")
                return null;
            else
            {
                var s = obj.ToString().Split('/');
                if (s[0].Length == 4)
                    return new DateTime(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
                else
                    return new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
            }
        }
        public static DateTime revercedate(object obj)
        {
            var s = obj.ToString().Split('/');
            if (s[0].Length == 4)
                return new DateTime(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
            else
                return new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
        }



        public static DateTime GetCurrentPersianDateTime()
        {
            PersianDateTime ps = new PersianDateTime(DateTime.Now);
            DateTime dd = new DateTime(ps.Year, ps.Month, ps.Day, ps.Hour, ps.Minute, ps.Second);
            return dd;
        }
        public static DateTime? GetPersianDateTime(DateTime? dt)
        {
            if (dt.HasValue)
            {
                try
                {
                    PersianDateTime ps = new PersianDateTime(dt.Value);
                    DateTime dd = new DateTime(ps.Year, ps.Month, ps.Day, ps.Hour, ps.Minute, ps.Second);
                    return dd;
                }
                catch
                {
                    return dt;
                }
            }
            else
                return dt;
        }

        public static DateTime GetPersianDateTime(DateTime dt)
        {
            try
            {
                PersianDateTime ps = new PersianDateTime(dt);
                DateTime dd = new DateTime(ps.Year, ps.Month, ps.Day, ps.Hour, ps.Minute, ps.Second);
                return dd;
            }
            catch
            {
                return dt;
            }
        }
        public static string ConvertToPersian(DateTime dateTime, string mod = "")
        {
            try
            {
                //SolarDate solar = Calendar.ConvertToPersian(dateTime);
                //return string.IsNullOrEmpty(mod) ? solar.ToString() : solar.ToString(mod);
                PersianDateTime ps = new PersianDateTime(dateTime);
                //DateTime dd = new DateTime(ps.Year, ps.Month, ps.Day, ps.Hour, ps.Minute, ps.Second);
                return ps.Year.ToString() + "/" + ps.Month.ToString() + "/" + ps.Day.ToString();// dd.ToShortDateString();
            }
            catch
            {
                return "";
            }
        }
        public static string ConvertToLongPersian(DateTime dateTime, string mod = "")
        {
            try
            {
                //SolarDate solar = Calendar.ConvertToPersian(dateTime);
                //return string.IsNullOrEmpty(mod) ? solar.ToString() : solar.ToString(mod);
                PersianDateTime ps = new PersianDateTime(dateTime);
                //DateTime dd = new DateTime(ps.Year, ps.Month, ps.Day, ps.Hour, ps.Minute, ps.Second);
                return ps.Year.ToString() + "/" + ps.Month.ToString() + "/" + ps.Day.ToString() + "  " + ps.Hour.ToString() + ":"+ ps.Minute;// dd.ToShortDateString();
            }
            catch
            {
                return "";
            }
        }
        public static DateTime StringToDateTime(string d)
        {
            DateTime ret = new DateTime();
            if (!string.IsNullOrEmpty(d) && d.Contains("/") && d.Contains(":"))
            {
                string ss = "year= " + d.Substring(0, 4) + " month= " + d.Substring(d.IndexOf("/") + 1, d.LastIndexOf("/") - d.IndexOf("/") - 1).ToString() +
                     " day= " + d.Substring(d.LastIndexOf("/") + 1, 2) + " hour= " + d.Substring(d.IndexOf(":") - 2, 2).ToString() +
                     " minutes= " + d.Substring(d.IndexOf(":") + 1, 2).ToString(); // + " secount= " + viewModel.AddedDate.Second.ToString();
                PersianDateTime pdt = new PersianDateTime(int.Parse(d.Substring(0, 4)),
                       int.Parse(d.Substring(d.IndexOf("/") + 1, d.LastIndexOf("/") - d.IndexOf("/") - 1)),
                       int.Parse(d.Substring(d.LastIndexOf("/") + 1, 2)),
                       int.Parse(d.Substring(d.IndexOf(":") - 2, 2).ToString()),
                       int.Parse(d.Substring(d.IndexOf(":") + 1, 2)),
                       0);
                ret = pdt.ToDateTime();
            }
            return ret;
        }
        public static DateTime StringToPersianDateTime(string d)
        {
            DateTime ret = new DateTime();
            if (!string.IsNullOrEmpty(d) && d.Contains("/") && d.Contains(":"))
            {
                string ss = "year= " + d.Substring(0, 4) + " month= " + d.Substring(d.IndexOf("/") + 1, d.LastIndexOf("/") - d.IndexOf("/") - 1).ToString() +
                     " day= " + d.Substring(d.LastIndexOf("/") + 1, 2) + " hour= " + d.Substring(d.IndexOf(":") - 2, 2).ToString() +
                     " minutes= " + d.Substring(d.IndexOf(":") + 1, 2).ToString(); // + " secount= " + viewModel.AddedDate.Second.ToString();
                PersianDateTime ps = new PersianDateTime(int.Parse(d.Substring(0, 4)),
                       int.Parse(d.Substring(d.IndexOf("/") + 1, d.LastIndexOf("/") - d.IndexOf("/") - 1)),
                       int.Parse(d.Substring(d.LastIndexOf("/") + 1, 2)),
                       int.Parse(d.Substring(d.IndexOf(":") - 2, 2).ToString()),
                       int.Parse(d.Substring(d.IndexOf(":") + 1, 2)),
                       0);
                // ret = pdt.ToDateTime();
                // PersianDateTime ps = new PersianDateTime(DateAndTime.StringToDateTime(viewModel.AddedDate));
                ret = new DateTime(ps.Year, ps.Month, ps.Day, ps.Hour, ps.Minute, ps.Second);
            }
            return ret;
        }
    }
}