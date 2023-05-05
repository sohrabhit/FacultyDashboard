using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public class ExpressionMethods
    {
        public static bool ExpressionContainss(object FSource, string SearchedValue)
        {
            string Source = (FSource != null) ? FSource.ToString() : "";
            bool b = false;
            if (!string.IsNullOrEmpty(Source) && !string.IsNullOrEmpty(SearchedValue))
            {
                if (Source.Contains(",") && SearchedValue.Contains(","))
                {
                    var so = Source.Split(",");
                    var se = SearchedValue.Split(",");
                    foreach (var item in so)
                    {
                        foreach (var det in se)
                        {
                            if (det.Contains(item))
                                b = true;
                        }
                    }
                }
                else if (!Source.Contains(",") && SearchedValue.Contains(","))
                {
                    var se = SearchedValue.Split(",");
                    foreach (var det in se)
                    {
                        if (det.Contains(SearchedValue))
                            b = true;
                    }
                }
                else if (Source.Contains(",") && !SearchedValue.Contains(","))
                {
                    var se = Source.Split(",");
                    foreach (var det in se)
                    {
                        if (det.Contains(SearchedValue))
                            b = true;
                    }
                }
                else if (!Source.Contains(",") && !SearchedValue.Contains(","))
                {
                    if (Source.Contains(SearchedValue))
                        b = true;
                }
            }
            return b;
            //return Source.Trim().Contains(SearchedValue.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
