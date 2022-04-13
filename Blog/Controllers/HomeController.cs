using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;


        public HomeController(ApplicationContext context)
        {
            db = context;

        }



        public async Task<IActionResult> Index(string email, string login, int? id, int page = 1, SortState sortOrder = SortState.IdAsc) //  - это файл окна, при открытии которого будет запускаться этот асинхрнный метод
        {
            IQueryable<User> users = db.Users;
            IQueryable<Role> roles = db.Roles;
            //Фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.ID == id);
            }
            if (!String.IsNullOrEmpty(email))
            {
                users = users.Where(p => p.Email.Contains(email));
            }
            if (!String.IsNullOrEmpty(login))
            {
                users = users.Where(p => p.Login.Contains(login));
            }

            //Сортировка
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(m => m.ID);
                        // Activity activity = new Activity("Index");

                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(m => m.ID);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(m => m.Email);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(m => m.Email);
                        break;
                    }
                case SortState.LoginAsc:
                    {
                        users = db.Users.OrderBy(m => m.Login);
                        break;
                    }
                case SortState.LoginDesc:
                    {
                        users = users.OrderByDescending(m => m.Login);
                        break;
                    }
                default:
                    {
                        users = users.OrderBy(m => m.Email);
                        break;
                    }
            }
            //Пагинация
            int pageSize = 5; //Количество строк, которые будут выводиться на одной странице
            var count = await users.CountAsync();
            var item = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var role = await roles.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(id, email, login),
                Users = item,
                Roles = role
            };

            return View(viewModel);
        }

        //public IActionResult  ()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel user)
        {
            //if (user.Email != "" || user.Login != "" ||
            //    user.Password != "" || user.Name != "" ||
            //    user.Surname != "" || user.BirthDate.Equals(""))
            //{
            if (ModelState.IsValid)
            {
                //if (CheckUser(user))
                //{
                //1 - Администратор
                //2 - Пользователь

                if (db.Roles.CountAsync().Result == 0)
                {
                    db.Roles.Add(new Role() { Role_Name = "Администратор" });
                    db.Roles.Add(new Role() { Role_Name = "Пользователь" });
                    await db.SaveChangesAsync();
                }
                if (db.Users.CountAsync().Result == 0)
                {
                    db.Users.Add(new User()
                    {
                        Surname = "Админов",
                        Name = "Админ",
                        RoleID = 1,
                        //Role = db.Roles.Where(role => role.Role_Name == "Администратор").FirstOrDefault(),
                        Login = "admin",
                        Password = "admin"

                    }); ;
                    await db.SaveChangesAsync();
                }
                //Console.WriteLine(db.Roles.Find(0).Role_Name);
                //user.Role = db.Roles.Find(0);
                User u = db.Users.Where(o => o.Login == user.Login).FirstOrDefault();



                if (u == null)
                {
                    db.Users.Add(new User
                    {

                        Login = user.Login,
                        Password = user.Password,
                        Surname = user.Surname,
                        Name = user.Name,
                        BirthDate = user.BirthDate,
                        Email = user.Email,
                        RoleID = 2


                    });

                    await db.SaveChangesAsync();
                    return RedirectToAction("Loging");

                }
                else
                {

                    ModelState.AddModelError("Login", "Данный логин уже занят");
                    return View();
                }


                // Сделано выше! Надо сделать, чтобы при регистрации нового пользователя по дефолту присваивалась роль Пользователя                

                //}
                //else return View();

            }
            else return View();

        }

        public bool CheckUser(User user)
        {
            User u = db.Users.Where(o => o.Login == user.Login).FirstOrDefault();
            User u2 = db.Users.Where(o => o.Email == user.Email).FirstOrDefault();
            if (u != null || u2 != null)
                return false;
            else
                return true;
            //foreach(var us in db.Users)
            //{
            //    if (user.Email == us.Email || user.Login == us.Login)
            //        return false;
            //}

        }
        //Удаление с подтверждением
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        //[HttpGet] Удаление пользователя без подтверждения
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("GetAllUsers", new { id = GetLoggedUser().ID});
                }
            }
            return NotFound();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            user.RoleID = 2;
            db.Users.Update(user);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Loging()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Autorization()
        {
            return View();
        }
        //public IActionResult Account()
        //{
        //    return View();
        //}
        //public IActionResult Account(User user)
        //{
        //    return View(user);
        //}


        public bool GetLoggingUser(int? id)
        {
            User user = db.Users.FirstOrDefault(predicate => predicate.ID == id);
            string login = HttpContext.Request.Cookies["login"];
            string password = HttpContext.Request.Cookies["password"];

            if (user.Login == login && user.Password == password)
            {
                return true;
            }
            return false;
        }

        public User GetLoggedUser(int? id)
        {
            User user = db.Users.FirstOrDefault(predicate => predicate.ID == id);
            string login = HttpContext.Request.Cookies["login"];
            string password = HttpContext.Request.Cookies["password"];

            if (user.Login == login && user.Password == password)
            {
                return user;
            }
            return null;
        }


        public async Task<IActionResult> Account(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {
                    SetPostUser(user.ID);

                    return View(new IndexViewModel { ViewingUser = user, RequesterUser = GetLoggingUser(id), LoggedUser = GetLoggedUser() });
                }
            }
            return NotFound();
        }

        public User GetLoggedUser()
        {
            string login = HttpContext.Request.Cookies["login"];
            string password = HttpContext.Request.Cookies["password"];
            return db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
        }



        [HttpGet]
        public async Task<IActionResult> AccountEdit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {

                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AccountEdit(User user)
        {
            //user.RoleID = GetLoggedUser().RoleID;

            User oldUser = await db.Users.Where(u => u.ID == user.ID).FirstOrDefaultAsync();

            oldUser.Login = user.Login;
            oldUser.Name = user.Name;
            oldUser.Surname = user.Surname;
            oldUser.Password = user.Password;
            oldUser.Email = user.Email;
            oldUser.BirthDate = user.BirthDate;

            //Ошибка при обновлении данных 
            db.Users.Update(oldUser);

            await db.SaveChangesAsync();
            return RedirectToAction("Account", new { id = user.ID });
        }

        [HttpGet]
        public async Task<IActionResult> EmptyIndex()
        {
            string login = HttpContext.Request.Cookies["login"];
            string password = HttpContext.Request.Cookies["password"];

            User loggedUser = await db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefaultAsync();

            if (loggedUser == null)
            {
                return RedirectToAction("Loging", "Home");
            }
            return RedirectToAction("Account", new { id = loggedUser.ID });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int? id)
        {

            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {

                    return View("AllUsers", new IndexViewModel { ViewingUser = user, RequesterUser = GetLoggingUser(id), LoggedUser = GetLoggedUser(), AllUsers = db.Users.ToList() });
                }
            }
            return NotFound();

        }



        //public async Task<IActionResult> Account(User user)
        //{

        //        if (user != null)
        //        {
        //            SetPostUser(user.ID);
        //            return View(new IndexViewModel { ViewingUser = user, RequesterUser = GetLoggingUser(user) });
        //        }

        //    return NotFound();
        //}
        public void SetPostUser(int userId)
        {
            User user = db.Users.Where(u => u.ID == userId).FirstOrDefault();

            foreach (Post post in db.Posts)
            {
                if (post.UserID == userId)
                {

                    user.Posts.Add(post);
                }
            }
            db.SaveChanges();
        }

        [HttpPost]
        public async Task<IActionResult> Loging(User user)
        {
            if (ModelState.IsValid)
            {


                User u = db.Users.Where(o => o.Login == user.Login).FirstOrDefault();

                if (u != null)
                {
                    if (u.Password == user.Password)
                    {
                        Console.WriteLine("USER CHECK" + u.ID + " - " + u.Email + " : " + u.Role);
                        string roleName = db.Roles.Find(u.RoleID).Role_Name;

                        //if (u.Role?.Role_Name == roleName)
                        //{
                            CookieOptions cookie = new CookieOptions();
                            cookie.Expires = DateTime.Now.AddMinutes(30);
                            Response.Cookies.Append("login", user.Login, cookie);
                            Response.Cookies.Append("password", user.Password, cookie);

                            return RedirectToAction("Account", new { id = u.ID });
                        //}
                        //else return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Неверный пароль");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Аккаунта с таким логином не существует!");
                    return View();
                }
            }
            else return View();
        }





        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.ID == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
    }
}
