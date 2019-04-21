using LambdaForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForum.Data
{
    public interface IForum
    {
        //Some API/implementation of the interaction with the forum entity model
        //in this application 

        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);





    }
}
