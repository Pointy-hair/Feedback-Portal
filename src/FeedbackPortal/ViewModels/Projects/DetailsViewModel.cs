using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.ViewModels.Common;
using FeedbackPortal.ViewModels.Issues;

namespace FeedbackPortal.ViewModels.Projects
{
    public class DetailsViewModel
    {
        public ProjectModel Project { get; set; }
        public UserModel OwnerUser { get; set; }

        public IEnumerable<IssueModel> Issues { get; set; }

        public IEnumerable<UserModel> UserLookup { get; set; }

    }
}
