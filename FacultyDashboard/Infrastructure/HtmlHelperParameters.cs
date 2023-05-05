using Common.Utilities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace FacultyDashboard.Infrastructure
{
    public static class HtmlHelperParameters
    {
        public static IHtmlContent ConvertToShortPersianDateTime(this IHtmlHelper html, DateTime? dateTime)
        {
            string result;
            result = dateTime.HasValue ? DateAndTime.ConvertToPersian(dateTime.Value) : "";
            return new HtmlString(result);
        }
        public static IHtmlContent ShowShortPersianDateTime(this IHtmlHelper html, DateTime? dateTime)
        {
            string result;
            result = dateTime.HasValue ? dateTime.Value.Year + "/" + dateTime.Value.Month + "/" + dateTime.Value.Day + " " + dateTime.Value.Hour + ":" + dateTime.Value.Minute: "";
            return new HtmlString(result);
        }
        public static IHtmlContent ConvertToLongPersianDateTime(this IHtmlHelper html, DateTime? dateTime)
        {
            string result;
            result = dateTime.HasValue ? DateAndTime.ConvertToLongPersian(dateTime.Value) : "";
            return new HtmlString(result);
        }
        public static IHtmlContent ConvertToShortMiladiDateTime(this IHtmlHelper html, DateTime? dateTime)
        {
            string result;
            result = dateTime.HasValue ? dateTime.Value.ToShortDateString() : "";
            return new HtmlString(result);
        }
        public static IHtmlContent ConvertToString(this IHtmlHelper html, DateTime? dateTime)
        {
            string result = "";
            //var span = new TagBuilder("span");
            //span.InnerHtml.Append("Hello, " + name + "!");

            //var br = new TagBuilder("br") { TagRenderMode = TagRenderMode.SelfClosing };
            //using (var writer = new StringWriter())
            //{
            //    span.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            //    br.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            //    result = writer.ToString();
            //}

            return new HtmlString(result);
        }
    }
}
