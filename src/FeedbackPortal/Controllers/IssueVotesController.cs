using System;
using System.Threading.Tasks;
using FeedbackPortal.Data;
using FeedbackPortal.Extensions;
using FeedbackPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbackPortal.Controllers
{
    [Authorize]
    [Route("projects/{projectKey}/issues/{issueId}")]
    public class IssueVotesController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public IssueVotesController(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
            : base(userManager, signInManager)
        {
            _dbContext = dbContext;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Vote(string projectKey, int issueId)
        {

            var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == projectKey);
            if (project == null)
            {
                this.ErrorNotice("Project not found");
                return RedirectToAction("Index", "Projects");
            }

            var issue = await _dbContext.Issues.SingleOrDefaultAsync(x => x.Id == issueId);
            if (issue == null || issue.ProjectId != project.Id)
            {
                this.ErrorNotice("Issue not found");
                return RedirectToAction("Details", "Projects", new { key = project.Key });
            }

            throw new NotImplementedException();
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> RemoveVote(string projectKey, int issueId)
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


            throw new NotImplementedException();
        }
    }
}