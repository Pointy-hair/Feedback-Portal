using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackPortal.Data;
using FeedbackPortal.Extensions;
using FeedbackPortal.Models;
using FeedbackPortal.Models.Projects;
using FeedbackPortal.ViewModels.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FeedbackPortal.Controllers
{
    [Authorize]
    [Route("projects")]
    public class ProjectsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ProjectsController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext,
            ILoggerFactory loggerFactory)
            : base (userManager, signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<ProjectsController>();
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var model = new ProjectListViewModel();

            var projects = await _dbContext.Projects.Select(x => x.ToModel()).ToListAsync();
            model.Projects = projects;

            return View(model);
        }

        [Route("{key}")]
        public async Task<IActionResult> Details(string key)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == key);
            if (project == null)
            {
                this.ErrorNotice("Project could not be found");
                return RedirectToAction(nameof(Index));
            }

            var model = new DetailsViewModel();
            model.Project = project.ToModel();

            var owner = await _userManager.FindByIdAsync(project.OwnerUserId);
            model.OwnerUser = owner.ToModel();

            var issues = await _dbContext.Issues
                .Where(x => x.ProjectId == project.Id)
                .Select(x => x.ToModel())
                .ToListAsync();
            model.Issues = issues;

            model.UserLookup = await _dbContext.Users
                .Select(x => x.ToModel())
                .ToListAsync();

            return View(model);
        }

        [Route("create")]
        public IActionResult Create()
        {
            var model = new EditProjectViewModel();
            
            return View(model);
        }

        [Route("create"), HttpPost]
        public async Task<IActionResult> Create(EditProjectViewModel model)
        {
            try
            {
                var project = new Project
                    {
                        Name = model.Name,
                        Key = model.Key, // TODO: validate slug
                        Description = model.Description,
                        Url = model.Url,
                        CreatedOnUtc = DateTime.UtcNow,
                        OwnerUser = await GetCurrentUserAsync()
                    };

                _dbContext.Projects.Add(project);
                await _dbContext.SaveChangesAsync();

                this.SuccessNotice("The project has been created");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(5001, ex, ex.Message, model);

                this.ErrorNotice("An error occurred creating the project.");
                return View(model);
            }
        }
    }
}