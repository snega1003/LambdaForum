using LambdaForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForum.Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForumId(int id);
        IEnumerable<Post> GetPostsByUserId(int id);
        IEnumerable<Post> GetLatestPosts(int n);
        IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts);

        string GetForumImageUrl(int id);

        Task Add(Post post);
        Task Archive(int id);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);

        Task AddReply(PostReply reply);

        int GetReplyCount(int id);

    }
}
