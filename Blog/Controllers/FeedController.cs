using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class FeedController : Controller
    {
        private readonly ApplicationContext db;
        public FeedController(ApplicationContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            return View("Feed", new FeedViewModel{ Feed = db.Posts.ToList(), DataBase = db , LoggedUser = GetLoggedUser()});
        }

        private User GetLoggedUser()
        {
            string login = HttpContext.Request.Cookies["login"];
            string password = HttpContext.Request.Cookies["password"];
            return db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllPosts(int? id)
        //{

        //    if (id != null)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
        //        if (user != null)
        //        {

        //            return View("Feed", new IndexViewModel { ViewingUser = user, LoggedUser = GetLoggedUser(),  = db.Users.ToList() });
        //        }
        //    }
        //    return NotFound();

        //}

        //public User GetLoggedUser()
        //{
        //    string login = HttpContext.Request.Cookies["login"];
        //    string password = HttpContext.Request.Cookies["password"];
        //    return db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
        //}
    }
}
