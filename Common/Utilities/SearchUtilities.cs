using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public class SearchUtilities
    {
        public static List<Tuple<string, string, object>> SearchUrlParameters(string urlQueryString)
        {
            List<Tuple<string, string, object>> SearchParameters = new List<Tuple<string, string, object>>();
            if (!string.IsNullOrEmpty(urlQueryString) && urlQueryString.Contains("&"))
            {
                // 1 => FieldName
                // 2 => Operator
                // 3 => Value
                var query = urlQueryString.Split("&");
                List<string> fields = new List<string>();
                List<string> ops = new List<string>();
                List<string> vals = new List<string>();
                foreach (var item in query)
                {
                    var n = item.Split("=");
                    if (n[0] == "field")
                        fields.Add(n[1]);
                    else if (n[0] == "op")
                        ops.Add(n[1]);
                    else if (n[0] == "value")
                        vals.Add(n[1]);
                }
                for (int i = 0; i < fields.Count; i++)
                {
                    SearchParameters.Add(new Tuple<string, string, object>(fields[i], ops[i], vals[i]));
                }
            }
            return SearchParameters;
        }
    }
}
