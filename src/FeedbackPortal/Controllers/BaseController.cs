using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackPortal.Extensions;
using FeedbackPortal.Models;
using FeedbackPortal.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackPortal.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        protected BaseController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        protected Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

       
    }
}
