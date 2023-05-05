using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyDashboard.Infrastructure
{
    public static class EnumSelector
    {
        public static T ParseEnum<T>(object value)
        {
            return (T)Enum.Parse(typeof(T), value.ToString(), true);
        }
        public static string GetEnumDisplayNames(Type enum1, object val)
        {
            //val = "1,2,4";
            string res = "";
            if (val == null)
                return "";
            else if (!string.IsNullOrEmpty(val.ToString()))
            {
                if (val.ToString().Contains(","))
                {
                    string[] v = val.ToString().Split(',');
                    foreach (string cc in v)
                    {
                        if (!string.IsNullOrEmpty(cc))
                        {
                            var field = enum1.GetField(cc.ToString());
                            var attr = field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
                            res += attr != null ? attr.Name : "";// enu.ToString();
                        }
                    }
                }
                else
                {
                    var field = enum1.GetField(val.ToString());
                    var attr = field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
                    res = attr != null ? attr.Name : "";// enu.ToString();
                                                        // res = Enum.GetName(enum1, int.Parse(val.ToString()));
                }
            }
            return res;
        }
        public static string GetEnumValues(Type enum1, object val)
        {
            //val = "1,2,4";
            string res = "";
            if (val == null)
                return "";
            else if (!string.IsNullOrEmpty(val.ToString()))
            {
                if (val.ToString().Contains(","))
                {
                    string[] v = val.ToString().Split(',');
                    foreach (string cc in v)
                    {
                        if (!string.IsNullOrEmpty(cc))
                            res += Enum.GetName(enum1, int.Parse(cc));
                    }
                }
                else
                    res = Enum.GetName(enum1, int.Parse(val.ToString()));
            }
            return res;
        }
        public static List<string> GetEnumValuesList(Type enum1, string val)
        {
            //val = "1,2,4";
            List<string> res = new List<string>();
            if (!string.IsNullOrEmpty(val))
            {
                if (val.Contains(","))
                {
                    string[] v = val.Split(',');
                    foreach (string cc in v)
                    {
                        if (!string.IsNullOrEmpty(cc))
                            res.Add(Enum.GetName(enum1, int.Parse(cc)));
                    }
                }
                else
                    res.Add(Enum.GetName(enum1, int.Parse(val)));
            }
            return res;
        }
        public static IEnumerable<SelectListItem> GetEnumItems(
             this Type enumType, object selectedValue)
        {
            //selectedValue = "1,2,4";
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            if (selectedValue != null && !string.IsNullOrEmpty(selectedValue.ToString()))
            {
                if (selectedValue.ToString().Contains(","))
                {
                    IList<string> selectedvals = selectedValue.ToString().Split(',').ToList();
                    return names.Zip(values, (name, value) =>
                                new SelectListItem
                                {
                                    Text = GetName(enumType, name),
                                    Value = value.ToString(),
                                    Selected = selectedvals.Contains(value.ToString())//value == int.Parse(c)
                                });
                }
                else
                {
                    return names.Zip(values, (name, value) =>
                                new SelectListItem
                                {
                                    Text = GetName(enumType, name),
                                    Value = value.ToString(),
                                    Selected = value == int.Parse(selectedValue.ToString())
                                });
                }
            }
            else
                return names.Zip(values, (name, value) =>
                                new SelectListItem
                                {
                                    Text = GetName(enumType, name),
                                    Value = value.ToString(),
                                    Selected = false
                                });
        }
        static string GetName(Type enumType, string name)
        {
            var result = name;

            var attribute = enumType
                .GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }


    }
}