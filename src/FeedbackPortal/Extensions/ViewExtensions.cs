using System;
using System.Collections.Generic;
using FeedbackPortal.Models.Issues;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace FeedbackPortal.Extensions
{
    public static class ViewExtensions
    {
        private const string TitleViewDataKey = "Title";
        private const string SubTitleViewDataKey = "SubTitle";

        public static void SetTitle(this IHtmlHelper htmlHelper, string title)
        {
            htmlHelper.ViewData[TitleViewDataKey] = title;
        }

        public static void SetSubTitle(this IHtmlHelper htmlHelper, string subTitle)
        {
            htmlHelper.ViewData[SubTitleViewDataKey] = subTitle;
        }

        public static bool HasSubTitle(this IHtmlHelper htmlHelper)
        {
            return htmlHelper.ViewData[SubTitleViewDataKey] != null;
        }

        public static string Title(this IHtmlHelper htmlHelper)
        {
            return htmlHelper.ViewData[TitleViewDataKey] as string;
        }

        public static string SubTitle(this IHtmlHelper htmlHelper)
        {
            if (!htmlHelper.HasSubTitle())
                return string.Empty;

            return htmlHelper.ViewData[SubTitleViewDataKey] as string;
        }

        public static void Put<T>(this ITempDataDictionary td, string key, T data) where T : class, new()
        {
            td[key] = JsonConvert.SerializeObject(data);
        }

        public static T Get<T>(this ITempDataDictionary td, string key, bool useDefaultIfNull = false) where T : class
        {
            object obj;
            td.TryGetValue(key, out obj);

            if (obj == null && useDefaultIfNull)
                return default(T);
            else if (obj == null)
                return null;

            var result = JsonConvert.DeserializeObject<T>((string) obj);
            return result;
        }
        
        public static IEnumerable<SelectListItem> ToSelectList<T>(this T en)// where T : struct, IConvertible
        {
            var vals = Enum.GetValues(typeof(T));
            var opts = new List<SelectListItem>();

            foreach (var val in vals)
            {
                var opt = new SelectListItem
                    {
                        Value = val.ToString(),
                        Text = Enum.GetName(typeof(T), val)
                    };
                opts.Add(opt);
            }

            return opts;
        }

        public static IEnumerable<SelectListItem> IssueTypesToSelectList(this IHtmlHelper helper)
        {
            var opts = new List<SelectListItem>();

            foreach (var val in Enum.GetValues(typeof(IssueType)))
            {
                var item = (IssueType)val;
                var id = int.Parse(item.ToString("D"));
                var label = Enum.GetName(typeof(IssueType), val);

                var opt = new SelectListItem {Value = id.ToString(), Text = label};
                opts.Add(opt);
            }

            return opts;
        }

        public static IEnumerable<SelectListItem> IssueSeveritiesToSelectList(this IHtmlHelper helper)
        {
            var opts = new List<SelectListItem>();

            foreach (var val in Enum.GetValues(typeof(IssueSeverity)))
            {
                var item = (IssueSeverity)val;
                var id = int.Parse(item.ToString("D"));
                var label = Enum.GetName(typeof(IssueSeverity), val);

                var opt = new SelectListItem { Value = id.ToString(), Text = label };
                opts.Add(opt);
            }

            return opts;
        }

        public static IEnumerable<SelectListItem> IssueStatusesToSelectList(this IHtmlHelper helper)
        {
            var opts = new List<SelectListItem>();

            foreach (var val in Enum.GetValues(typeof(IssueStatus)))
            {
                var item = (IssueStatus)val;
                var id = int.Parse(item.ToString("D"));
                var label = Enum.GetName(typeof(IssueStatus), val);

                var opt = new SelectListItem { Value = id.ToString(), Text = label };
                opts.Add(opt);
            }

            return opts;
        }

    }

    public static class IssueViewExtensions
    {
        //public static HtmlString IssueStatusLabel(this IssueStatus status)
        //{
        //    var tb = new TagBuilder("span");
        //    tb.SetInnertText("")
        //}
    }
}
