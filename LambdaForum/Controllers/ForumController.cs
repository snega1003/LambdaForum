using LambdaForum.Data;
using LambdaForum.Models.Forum;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LambdaForum.Controllers
{
    public class ForumController : Controller
    {

        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            //forum -- коллекция объектов ForumListingModel (var = IEnumerable<ForumListingModel>)
            //и возвращаться должен тот самый объект (new ForumListingModel)
            var forums = _forumService
                .GetAll()
                .Select(forum => new ForumListingModel {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description

                });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
            var posts = _postService.GetFilteredPost(id);

            var postListings = 
        }
    }
}