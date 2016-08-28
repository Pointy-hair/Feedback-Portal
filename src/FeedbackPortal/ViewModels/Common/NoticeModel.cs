namespace FeedbackPortal.ViewModels.Common
{
    public class NoticeModel
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public NoticeModel(string type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
