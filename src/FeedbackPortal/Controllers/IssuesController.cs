using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.Data;
using FeedbackPortal.Extensions;
using FeedbackPortal.Models;
using FeedbackPortal.Models.Issues;
using FeedbackPortal.ViewModels.Issues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FeedbackPortal.Controllers
{
    [Authorize]
    [Route("projects/{projectKey}/issues")]
    public class IssuesController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public IssuesController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext,
            ILoggerFactory loggerFactory)
             : base(userManager, signInManager)
        {
            _dbContext = dbContext;

            _logger = loggerFactory.CreateLogger<IssuesController>();
        }

        [Route("{id}")]
        public async Task<IActionResult> Details(string projectKey, int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == projectKey);
            if (project == null)
            {
                this.ErrorNotice("Project not found");
                return RedirectToAction("Index", "Projects");
            }

            var issue = await _dbContext.Issues.SingleOrDefaultAsync(x => x.Id == id);
            if (issue == null || issue.ProjectId != project.Id)
            {
                this.ErrorNotice("Issue not found");
                return RedirectToAction("Details", "Projects", new {key = project.Key});
            }

            var model = new DetailsViewModel
                {
                    Project = project.ToModel(),
                    Issue = issue.ToModel()
                };

            model.UserLookup = await _dbContext.Users
                .Select(x => x.ToModel())
                .ToListAsync();

            return View(model);
        }

        [Route("create")]
        public async Task<IActionResult> Create(string projectKey)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == projectKey);
            if (project == null)
            {
                this.ErrorNotice("Project could not be found");
                return RedirectToAction("Details", "Projects", new {key = projectKey});
            }

            var model = new EditIssueViewModel
                {
                    ProjectId = project.Id,
                    Project = project.ToModel()
                };

            return View(model);
        }

        [Route("create"), HttpPost]
        public async Task<IActionResult> Create(string projectKey, EditIssueViewModel model)
        {
            try
            {

                var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == projectKey);
                var issue = new Issue
                    {
                        Project = project,
                        CreatedUser = await GetCurrentUserAsync(),
                        CreatedOnUtc = DateTime.UtcNow,
                        Type = model.Type,
                        Severity = model.Severity,
                        Status = model.Status,
                        Title = model.Title,
                        Description = model.Description
                    };

                _dbContext.Issues.Add(issue);
                await _dbContext.SaveChangesAsync();

                this.SuccessNotice("The issue has been created");
                return RedirectToAction("Details", "Projects", new {key = projectKey});
            }
            catch (Exception ex)
            {
                _logger.LogError(302, ex, ex.Message);

                this.ErrorNotice("There was an error creating the issue.  Try again.");
                return View(model);
            }
        }
    }
}