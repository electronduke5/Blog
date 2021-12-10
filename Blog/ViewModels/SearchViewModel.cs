using Blog.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class SearchViewModel 
    {
        public string? SelectQuery { get; private set; }
        //public string SelectSurname { get; private set; }
        //public string SelectName { get; private set; }

        //public SearchViewModel(string surname, string name)
        //{
        //    SelectSurname = surname;
        //    SelectName = name;
        //}
        public SearchViewModel(string query)
        {
            SelectQuery = query;
        }
        public SearchViewModel() { }

        public IEnumerable<User> Found { get; set; } = new List<User>();

        //public SearchViewModel(HttpContext httpContext, ApplicationContext db)
        //    :base (httpContext, db)
        //{
        //}
    }
}
