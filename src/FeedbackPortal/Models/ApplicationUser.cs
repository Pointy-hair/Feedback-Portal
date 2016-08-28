using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FeedbackPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
