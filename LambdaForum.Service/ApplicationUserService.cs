using LambdaForum.Data;
using LambdaForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForum.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(ApplicationUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task BumpRating(string userId, Type type)
        {
            var user = GetById(userId);
            var increment = GetIncrement(type);
            user.Rating += increment;
            await _context.SaveChangesAsync();
        }

        private static int GetIncrement(Type type)
        {
            var bump = 0;
            
            if (type == typeof(Post))
            {
                bump = 3;
            }

            if (type == typeof(PostReply))
            {
                bump = 2;
            }
            return bump;
        }

        public Task Deactivate(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(
                user => user.Id == id);
        }

        public async Task IncrementRating(string id, Type type)
        {
            var user = GetById(id);
            user.Rating += 1;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
