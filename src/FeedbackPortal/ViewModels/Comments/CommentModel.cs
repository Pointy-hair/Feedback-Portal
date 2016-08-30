using System;
using FeedbackPortal.ViewModels.Common;

namespace FeedbackPortal.ViewModels.Comments
{
    public class CommentModel
    {
        public int Id { get; set; }

        public int IssueId { get; set; }
        
        public string CreatedUserId { get; set; }
        public UserModel PostedByUser { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string Text { get; set; }
    }
}
