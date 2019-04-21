using Microsoft.AspNetCore.Identity;

namespace LambdaForum.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual string Mail_not_necessary { get; set; }
    }
}
