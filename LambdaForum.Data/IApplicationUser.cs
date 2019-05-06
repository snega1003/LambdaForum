using LambdaForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LambdaForum.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task SetProfileImage(string id, Uri uri);
        Task IncrementRating(string id, Type type);
    }
}
