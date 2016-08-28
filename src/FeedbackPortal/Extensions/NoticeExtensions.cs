using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackPortal.Extensions
{
    public static class NoticeExtensions
    {
        public static void SuccessNotice(this Controller controller, string message)
        {
            AddNotice(controller, "success", message);
        }

        public static void ErrorNotice(this Controller controller, string message)
        {
            AddNotice(controller, "danger", message);
        }

        public static void WarningNotice(this Controller controller, string message)
        {
            AddNotice(controller, "warning", message);
        }

        public static void InfoNotice(this Controller controller, string message)
        {
            AddNotice(controller, "info", message);
        }

        private static void AddNotice(Controller controller, string type, string message)
        {
            var notices = controller.TempData.Get<List<NoticeModel>>("_notices") ?? new List<NoticeModel>();
            notices.Add(new NoticeModel(type, message));
            controller.TempData.Put("_notices", notices);
        }
    }
}
