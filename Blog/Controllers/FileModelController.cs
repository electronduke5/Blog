//using Blog.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using Microsoft.EntityFrameworkCore;

//namespace Blog.Controllers
//{
//    public class FileModelController : Controller
//    {
//        private ApplicationContext db;
//        private IWebHostEnvironment _app;

//        public FileModelController(ApplicationContext context, IWebHostEnvironment app)
//        {
//            db = context;
//            _app = app;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }


//        [HttpGet]
//        public async Task<IActionResult> AddFile()
//        {
//            return View(await db.FileModels.ToListAsync());
//        }


//        [HttpPost]
//        public async Task<IActionResult> AddFile( IFormFile? file)
//        {
//            if(file != null)
//            {
//                string path = $"/Files/{file.FileName}";
//                using (FileStream fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
//                {
//                    await file.CopyToAsync(fileStream);
//                }
//                FileModel fileModel = new FileModel
//                {
//                    Name = file.FileName,
//                    Path = path
//                };

//                db.FileModels.Add(fileModel);

//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return RedirectToAction("AddFile");
//        }
//    }
//}
