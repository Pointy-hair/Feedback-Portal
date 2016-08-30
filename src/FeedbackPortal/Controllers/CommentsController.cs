using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.Data;
using FeedbackPortal.Extensions;
using FeedbackPortal.Models;
using FeedbackPortal.Models.Comments;
using FeedbackPortal.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FeedbackPortal.Controllers
{
    [Authorize]
    [Route("projects/{projectKey}/issues/{issueId}/comments")]
    public class CommentsController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public CommentsController(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
            : base(userManager, signInManager)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<CommentsController>();
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Get(string projectKey, int issueId, string format="html")
        {
            var comments = await _dbContext.Comments
                .Where(x => x.IssueId == issueId)
                .OrderByDescending(x => x.CreatedOnUtc)
                .Select(x => x.ToModel())
                .ToListAsync();

            var userIds = comments.Select(x => x.CreatedUserId).Distinct();

            var userLookup = _dbContext.Users.Where(x => userIds.Contains(x.Id))
                .Select(x => x.ToModel())
                .ToList();

            foreach (var comment in comments)
            {
                comment.PostedByUser = userLookup.SingleOrDefault(x => x.Id == comment.CreatedUserId);
            }

            switch (format)
            {
                case "json":
                    return Json(comments);
                //case "html":
                default:
                    return PartialView("_CommentList", comments);
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Create(string projectKey, int issueId, CommentModel model)
        {
            try
            {
                var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == projectKey);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var issue = await _dbContext.Issues.SingleOrDefaultAsync(x => x.Id == issueId);
                if (issue == null || issue.ProjectId != project.Id)
                {
                    return new NotFoundResult();
                }

                var comment = new Comment
                    {
                        IssueId = issueId,
                        CreatedUser = await GetCurrentUserAsync(),
                        CreatedOnUtc = DateTime.UtcNow,
                        Text = model.Text
                    };

                _dbContext.Comments.Add(comment);

                issue.CommentCount += 1;

                await _dbContext.SaveChangesAsync();

                model.Id = comment.Id;
                return Json(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(501, ex, ex.Message, model);

                return new StatusCodeResult(500);
            }
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: Allow project owners to delete others' comments

            try
            {
                var comment = await _dbContext.Comments.SingleOrDefaultAsync(x => x.Id == id);
                if (comment == null)
                {
                    return new NotFoundResult();
                }

                var currentUser = await GetCurrentUserAsync();
                if (comment.CreatedUserId != currentUser.Id)
                {
                    return new UnauthorizedResult();
                }

                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();

                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(503, ex, "Error deleting comment: " + ex.Message, new {id});
                return new StatusCodeResult(500);
            }
        }
    }
}