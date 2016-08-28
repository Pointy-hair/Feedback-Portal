using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FeedbackPortal.Models.Projects;

namespace FeedbackPortal.Models.Issues
{
    public class Issue
    {
        public int Id { get; set; }
        
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Required, MaxLength(450)]
        public string CreatedUserId { get; set; }
        [ForeignKey("CreatedUserId")]
        public virtual ApplicationUser CreatedUser { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public IssueType Type { get; set; }
        public IssueSeverity Severity { get; set; }
        public IssueStatus Status { get; set; }

        [Required, MaxLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual List<IssueVote> Votes { get; set; }
        public int VoteCount { get; set; }

        public Issue()
        {
            Type = IssueType.Comment;
            Severity = IssueSeverity.Normal;
            Status = IssueStatus.New;

            Votes = new List<IssueVote>();
        }
    }
}
