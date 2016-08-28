using System;
using System.Collections.Generic;
using FeedbackPortal.Extensions;
using FeedbackPortal.Models.Issues;

namespace FeedbackPortal.ViewModels.Issues
{
    public class IssueModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        
        public string CreatedUserId { get; set; }
        
        public DateTime CreatedOnUtc { get; set; }

        public IssueType Type { get; set; }
        public IssueSeverity Severity { get; set; }
        public IssueStatus Status { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int VoteCount { get; set; }

        public string GetCssClasses()
        {
            var cssClasses = Type.ToString().Clean()
                             + " " + Severity.ToString().Clean()
                             + " " + Status.ToString().Clean();
            return cssClasses;
        }
    }
}
