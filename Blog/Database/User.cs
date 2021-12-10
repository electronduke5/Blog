using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int ID { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

    public enum SortState
    {
        IdAsc,
        IdDesc,
        EmailAsc,
        EmailDesc,
        LoginAsc,
        LoginDesc
    }
}
