using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using LambdaForum.Data;
using LambdaForum.Service;
using LambdaForum.Data.Models;

namespace LambdaForum.Tests
{
    [TestFixture]
    [Category("Services")]
    public class PostServiceTests
    {
        [Test]
        public async Task Create_Post_Creates_New_Post_Via_Context()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Post_Writes_Post_To_Database").Options;

            // run the test against one instance of the context
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);

                var post = new Post()
                {
                    Title = "writing functional javascript",
                    Content = "some post content"
                };

                await postService.Add(post);
            }

            // use a separate instance of the context to verify correct data was saved to the db
            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.AreEqual(1, ctx.Posts.CountAsync().Result);
                Assert.AreEqual("writing functional javascript", ctx.Posts.SingleAsync().Result.Title);
            }
        }

        [Test]
        public void Get_Post_By_Id_Returns_Correct_Post()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id_Db").Options;

            using(var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post { Id = 1986, Title = "First post" });
                ctx.Posts.Add(new Post { Id = 223, Title = "Second post" });
                ctx.Posts.Add(new Post { Id = 12, Title = "Third post" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetById(223);
                Assert.AreEqual(result.Title, "Second post");
            }
        }

        [Test]
        public void Get_All_Post_Returns_All_Posts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Post_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post { Id = 1236, Title = "First post" });
                ctx.Posts.Add(new Post { Id = 4536, Title = "Second post" });
                ctx.Posts.Add(new Post { Id = 1002, Title = "Third post" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var res = postService.GetAll();
                Assert.AreEqual(3, res.Count());
            }
        }

        [Test]
        public async Task Checking_Reply_Count_Returns_Number_Of_Replies()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Search_Database").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post
                {
                    Id = 1234
                });

                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var post = postService.GetById(1234);

                await postService.AddReply(new PostReply
                {
                    Post = post,
                    Content = "Here's a post reply"
                });
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postSercive = new PostService(ctx);
                var replyCount = postSercive.GetReplyCount(1234);
                Assert.AreEqual(replyCount, 1);
            }
        }
    }
}
