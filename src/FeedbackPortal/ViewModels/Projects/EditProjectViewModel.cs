using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackPortal.ViewModels.Projects
{
    public class EditProjectViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Key")]
        [Required, MaxLength(100)]
        public string Key { get; set; }

        [Display(Name = "Name")]
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Project testing URL")]
        public string Url { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string OwnerUserId { get; set; }

        [Display(Name = "Disabled")]
        public bool IsDisabled { get; set; }
    }
}
