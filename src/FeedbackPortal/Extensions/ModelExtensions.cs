using System.Collections.Generic;
using System.Linq;
using FeedbackPortal.Models;
using FeedbackPortal.Models.Comments;
using FeedbackPortal.Models.Issues;
using FeedbackPortal.Models.Projects;
using FeedbackPortal.ViewModels.Comments;
using FeedbackPortal.ViewModels.Common;
using FeedbackPortal.ViewModels.Issues;
using FeedbackPortal.ViewModels.Projects;

namespace FeedbackPortal.Extensions
{
    public static class ModelExtensions
    {
        public static ProjectModel ToModel(this Project project)
        {
            var model = new ProjectModel
                {
                    Id = project.Id,
                    Key = project.Key,
                    Name = project.Name,
                    Description = project.Description,
                    Url = project.Url,
                    CreatedOnUtc = project.CreatedOnUtc,
                    OwnerUserId = project.OwnerUserId,
                    IsDisabled = project.IsDisabled
                };
            return model;
        }

        public static UserModel ToModel(this ApplicationUser user)
        {
            var model = new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName
                };
            return model;
        }

        public static IssueModel ToModel(this Issue issue)
        {
            var model = new IssueModel
                {
                    Id = issue.Id,
                    ProjectId = issue.ProjectId,
                    CreatedUserId = issue.CreatedUserId,
                    CreatedOnUtc = issue.CreatedOnUtc,
                    Type = issue.Type,
                    Severity = issue.Severity,
                    Status = issue.Status,
                    Title = issue.Title,
                    Description = issue.Description,
                    VoteCount = issue.VoteCount
                };
            return model;
        }

        public static CommentModel ToModel(this Comment comment)
        {
            var model = new CommentModel
                {
                    Id = comment.Id,
                    IssueId = comment.IssueId,
                    CreatedUserId = comment.CreatedUserId,
                    CreatedOnUtc = comment.CreatedOnUtc,
                    Text = comment.Text
                };
            return model;
        }

        public static string FindUserName(this IEnumerable<UserModel> users, string userId, string defaultIfUnknown = "unknown")
        {
            var user = users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return defaultIfUnknown;

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
