using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.Models.Issues;
using FeedbackPortal.ViewModels.Projects;

namespace FeedbackPortal.ViewModels.Issues
{
    public class EditIssueViewModel
    {
        public int Id { get; set; }
        
        public string CreatedUserId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        [Display(Name = "Type")]
        public IssueType Type { get; set; }
        [Display(Name = "Severity")]
        public IssueSeverity Severity { get; set; }
        [Display(Name = "Status")]
        public IssueStatus Status { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int VoteCount { get; set; }

        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }
    }
}
