using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class AllUsersViewModel
    {

        public AllUsersViewModel() { }

        public IEnumerable<User> AllUsers { get; set; } = new List<User>();
    }
}
