using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationContext db;

        public LoginController(ApplicationContext _db)
        {
            db = _db;
        }

        [HttpPost]
        public async Task<IActionResult> Loging(LoginViewModel user)
        {
            
            User u = db.Users.Where(o => o.Login == user.Login).FirstOrDefault();

            if (u != null && u.Password == user.Password)
            {
                Console.WriteLine("USER CHECK" + u.ID + " - " + u.Email + " : " + u.Role);
                string roleName = db.Roles.Find(u.RoleID).Role_Name;


                if (u.Role?.Role_Name == roleName)
                {
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Append("login", user.Login, cookie);
                    Response.Cookies.Append("password", user.Password, cookie);

                    return RedirectToAction("Account", new { id = u.ID });
                }
                else return RedirectToAction("Index");
            }
            return View();
        }
    }
}
