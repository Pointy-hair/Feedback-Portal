using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.ViewModels.Common;
using FeedbackPortal.ViewModels.Projects;

namespace FeedbackPortal.ViewModels.Issues
{
    public class DetailsViewModel
    {
        public ProjectModel Project { get; set; }
        public IssueModel Issue { get; set; }

        public IEnumerable<UserModel> UserLookup { get; set; }
    }
}
