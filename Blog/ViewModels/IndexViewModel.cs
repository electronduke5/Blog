using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<User> Users { get; set; }

        public User ViewingUser { get; set; }
        public bool RequesterUser { get; set; }
        public User LoggedUser { get; set; }
        public string ViewingFullName => $"{ViewingUser.Surname} {ViewingUser.Name}";

        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        public CreatePostViewModel CreatePostViewModel { get; set; } = new CreatePostViewModel();


        public PageViewModel PageViewModel { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
        public SearchViewModel SearchViewModel {get;set;}

        public IEnumerable<User> AllUsers { get; set; } = new List<User>();



    }
}
