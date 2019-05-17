using Microsoft.AspNetCore.Identity;
using System;

namespace LambdaForum.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSince { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string UserDescription { get; set; }
    }
}
