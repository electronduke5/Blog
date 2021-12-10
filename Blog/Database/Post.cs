using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public partial class Post
    {
        [Key]
        public int IdPost { get; set; }

        public string? ImagePath { get; set; }

        public string? Text { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;



        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }



    }
}
