using LambdaForum.Data;
using LambdaForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForum.Service
{
    public class ForumService : IForum
    {
        //Forum uses any framework to interact with actual data

        private readonly ApplicationDbContext _context;

        //Constructor
        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            //return any instance of a forum from database
            return _context.Forums
                .Include(forum => forum.Posts); 
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
