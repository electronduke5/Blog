using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Blog.Helpers
{
    public static class ImageHelper
    {
        public static async Task<Post> AddPost(this ApplicationContext source, IWebHostEnvironment appEnv, int userId, string? content, IFormFile? file)
        {
            string? imagePath = "";

            if(file != null)
            {
                imagePath = $"image{Path.GetExtension(file.FileName)}";
            }

            var newPost = new Post
            {
                UserID = userId,
                Text = content,
                ImagePath = imagePath,
                CreatedDate = DateTime.Now
            };

            source.Posts.Add(newPost);
            await source.SaveChangesAsync();

            if(file != null && imagePath != null)
            {
                var filePath = $"{appEnv.WebRootPath}/posts/id{userId}/post{newPost.IdPost}";

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                await using var fileStream = new FileStream($"{filePath}/{imagePath}", FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            //User u = source.Users.Where(o => o.ID == userId).FirstOrDefault();
            //u.Posts.Add(newPost);


            return newPost;
        }
    }
}
