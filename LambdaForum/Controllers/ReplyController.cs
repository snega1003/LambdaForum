using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForum.Data;
using LambdaForum.Data.Models;
using LambdaForum.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LambdaForum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        private readonly IApplicationUser _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReplyController(IForum forumService, 
            IPost postService, 
            IApplicationUser userService, 
            UserManager<ApplicationUser> userManager)
        {
            _forumService = forumService;
            _postService = postService;
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create(int id)
        {
            var post = _postService.GetById(id);
            var forum = _forumService.GetById(post.Forum.Id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,

                ForumId = forum.Id,
                ForumName = forum.Title,
                ForumImageUrl = forum.ImageUrl,

                AuthorId = user.Id,
                AuthorName = user.UserName,
                AuthorRating = user.Rating,
                AuthorImageUrl = user.ProfileImageUrl,
                IsAuthorAdmin = user.IsAdmin,

                Date = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);
            await _postService.AddReply(reply);
            await _userService.BumpRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });            
        }

        private PostReply BuildReply(PostReplyModel reply, ApplicationUser user)
        {
            //var now = DateTime.Now;
            var post = _postService.GetById(reply.Id);

            return new PostReply
            {
                Post = post,
                Content = post.Content,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}