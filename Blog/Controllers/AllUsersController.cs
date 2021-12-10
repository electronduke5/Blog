using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AllUsersController : Controller
    {
        private readonly ApplicationContext db;
        List<User> users = new List<User>();


        public AllUsersController(ApplicationContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            users = db.Users.ToList();

            //var view = new AllUsersViewModel()
            //{
            //    AllUsers = users
            //};

            var viewModel = new IndexViewModel()
            {
                AllUsers = users
            };
            return View(viewModel);
        }


        
    }
}
