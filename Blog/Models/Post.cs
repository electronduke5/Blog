using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public partial class Post
    {
        [NotMapped]
        public string FullImagePath => $"~/posts/id{UserID}/post{IdPost}/{ImagePath}";
    }
}
