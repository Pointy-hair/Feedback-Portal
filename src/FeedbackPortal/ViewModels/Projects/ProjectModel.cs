using System;

namespace FeedbackPortal.ViewModels.Projects
{
    public class ProjectModel
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string OwnerUserId { get; set; }
        
        public bool IsDisabled { get; set; }
    }
}
