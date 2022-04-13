using Blog.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public abstract class ViewModelBase
    {
        public User? RequesterUser { get; set; }
        public bool RequesterLoggedIn { get; set; }

        public string RequesterFullname
            => $"{RequesterUser?.Surname ?? "null"} {RequesterUser?.Name ?? "null"}";

        protected ViewModelBase()
        {
            RequesterUser = null;
            RequesterLoggedIn = false;
        }

        //protected ViewModelBase(HttpContext httpContext, ApplicationContext database)
        //{
        //    RequesterUser = database.TryGetLoggedUserSync(httpContext);
        //    RequesterLoggedIn = RequesterUser != null;
        //}

    }
}
