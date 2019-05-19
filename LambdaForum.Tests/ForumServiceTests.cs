using NUnit.Framework;
using LambdaForum.Data;
using Microsoft.EntityFrameworkCore;
using LambdaForum.Service;
using System.Linq;

namespace LambdaForum.Tests
{
    [TestFixture]
    [Category("Services")]
    public class ForumServiceTests
    {
        [Test]
        public void Filtered_Posts_Returns_Correct_Result_Count()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Search_Database").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Data.Models.Forum()
                {
                    Id = 25
                });

                ctx.Posts.Add(new Data.Models.Post()
                {
                    Forum = ctx.Forums.Find(25),
                    Id = 21341,
                    Title = "Functional programming",
                    Content = "Does anyone have experience deploying Haskell to production?"
                });

                ctx.Posts.Add(new Data.Models.Post()
                {
                    Forum = ctx.Forums.Find(25),
                    Id = -345,
                    Title = "Haskell Tail Recursion",
                    Content = "Haskell Haskell"
                });

                ctx.SaveChanges();
            }

            using(var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var forumService = new ForumService(ctx, postService);
                var postCount = forumService.GetFilteredPosts(25, "Haskell").Count();
                Assert.AreEqual(2, postCount);
            }
        }
    }
}