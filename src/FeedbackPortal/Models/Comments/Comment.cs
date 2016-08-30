using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FeedbackPortal.Models.Issues;

namespace FeedbackPortal.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int IssueId { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }

        [Required, MaxLength(450)]
        public string CreatedUserId { get; set; }
        [ForeignKey("CreatedUserId")]
        public virtual ApplicationUser CreatedUser { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        
        [Required]
        public string Text { get; set; }
    }
}
