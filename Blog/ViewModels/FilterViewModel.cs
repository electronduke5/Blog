using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class FilterViewModel
    {
        public int? SelectId { get; private set; }

        public string SelectEmail { get; private set; }

        public string SelectLogin { get; private set; }

        public FilterViewModel (int? id, string email, string login)
        {
            SelectId = id;
            SelectEmail = email;
            SelectLogin = login;
        }
    }
}
