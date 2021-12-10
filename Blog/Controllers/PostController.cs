using Blog.Models;
using Blog.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IWebHostEnvironment _appEnv;


        public PostController(ApplicationContext context, IWebHostEnvironment appEnv)
        {
            _db = context;
            _appEnv = appEnv;
        }


       

        [HttpPost]
        public async Task<IActionResult> CreatePost(int authorId, string? content, IFormFile? image)
        {
            if (content == null && image == null)
                ModelState.AddModelError("Content", "Вы же ничего не написали! Так не пойдет!");
            else
            {
               
                await _db.AddPost(_appEnv, authorId, content, image);
            }


            return RedirectToAction("Account", "Home", new { id = authorId });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int requesterId, int postId)
        {
            var post = await _db.Posts.FindAsync(postId);

            if(post != null)
            {
                await DeletePost(post);
            }
            return RedirectToAction("Account", "Home", new { id = requesterId });
        }

        private async Task DeletePost(Post post)
        {
            try
            {
                var directory = new DirectoryInfo($"{_appEnv.WebRootPath}/posts/id{post.UserID}/post{post.IdPost}");
                directory.Delete(true);
            }
            catch
            {
                //None
            }

            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
        }

    }
}
