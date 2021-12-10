using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class FeedViewModel
    {
        public ApplicationContext  DataBase { get; set; }
        public User LoggedUser { get; set; }
        public IEnumerable<Post> Feed { get; set; } = new List<Post>();
    }
}
