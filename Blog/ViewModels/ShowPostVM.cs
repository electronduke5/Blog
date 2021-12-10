using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class ShowPostVM
    {
        public int ID { get; set; }
        
        public string Text { get; set; }

        public string AuthorName { get; set; }

        public int AuthorID { get; set; }

        public DateTime DateCreated { get; set; }

        public string Date { get; set; }

        public bool IsMe { get; set; }
    }
}
