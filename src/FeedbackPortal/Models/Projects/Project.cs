using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackPortal.Models.Projects
{
    public class Project
    {
        public int Id { get; set; }
        
        [MaxLength(100), Required]
        public string Key { get; set; }

        [MaxLength(100), Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        [MaxLength(450), Required]
        public string OwnerUserId { get; set; }

        [ForeignKey("OwnerUserId")]
        public virtual ApplicationUser OwnerUser { get; set; }

        public bool IsDisabled { get; set; }
    }
}
