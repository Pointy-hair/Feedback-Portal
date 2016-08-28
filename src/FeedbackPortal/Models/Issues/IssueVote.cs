using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackPortal.Models.Issues
{
    public class IssueVote
    {
        [Key]
        public int IssueId { get; set; }

        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public VoteType VoteType { get; set; }
        public int Score => (int) VoteType;
    }
}
