using LambdaForum.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForum.Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);

        //Task AddReply(PostReply reply);
    }
}
