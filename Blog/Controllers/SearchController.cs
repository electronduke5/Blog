using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationContext db;
        List<User> users = new List<User>();


        public SearchController(ApplicationContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Search(
            [FromQuery] string? query
            )
        {
            var found = db.Users.ToList();

            if (query != null)
            {
                string[] args = query.ToLower().Split();
                foreach (var user in found)
                {
                    users.Add(user);
                }

                foreach (var arg in args)
                {
                    found.RemoveAll(
                        user =>
                        !user.Surname.ToLower().Contains(arg) &&
                        !arg.Contains(user.Surname.ToLower()) &&
                        !user.Name.ToLower().Contains(arg) &&
                        !arg.Contains(user.Name.ToLower())
                        );

                    users.RemoveAll(
                        user =>
                        !user.Surname.ToLower().Contains(arg) &&
                        !arg.Contains(user.Surname.ToLower()) &&
                        !user.Name.ToLower().Contains(arg) &&
                        !arg.Contains(user.Name.ToLower())
                        );
                    
                }
            }
            


            var viewModel = new SearchViewModel()
            {
                //Found = found.DistinctBy(profile => profile.IdProfile)


                Found = users
                
            };

            var indexVM = new IndexViewModel()
            {

                //Надо доабавтить войденного пользователя
                LoggedUser = GetLoggedUser(),

                SearchViewModel = viewModel
                
            };
            return View(indexVM);

            //[HttpGet]
            //public IActionResult Search(
            //    [FromQuery] string? query,
            //    [FromQuery] DateTime? birtday
            //    )
            //{
            //    var found = db.Users.ToList();

            //    if (query != null)
            //    {
            //        string[] args = query.ToLower().Split();

            //        foreach (var arg in args)
            //        {
            //            found.RemoveAll(
            //                user =>
            //                !user.Surname.ToLower().Contains(arg) &&
            //                !arg.Contains(user.Surname.ToLower()) &&
            //                !user.Name.ToLower().Contains(arg) &&
            //                !arg.Contains(user.Name.ToLower())
            //                );
            //        }
            //    }

            //    if (birtday != null)
            //    {
            //        found.RemoveAll(user => user.BirthDate == null || user.BirthDate != birtday);
            //    }

            //    var viewModel = new SearchViewModel(HttpContext, db)
            //    {
            //        //Found = found.DistinctBy(profile => profile.IdProfile)
            //        Found = (System.Collections.Generic.IEnumerable<User>)found.Select(user => user.ID).Distinct().ToList()
            //    };
            //    return View(viewModel);
        }
        public User GetLoggedUser()
        {
            string login = HttpContext.Request.Cookies["login"];
            string password = HttpContext.Request.Cookies["password"];
            return db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
        }
    }
}
