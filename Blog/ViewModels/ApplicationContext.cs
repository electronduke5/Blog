using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<FileModel> FileModels { get; set; }
        

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            :base(options)
        {
            Database.EnsureCreated();
        }

        public const string SessionLoggedUserIdKey = "LoggedUserId";




    }
}
