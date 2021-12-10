using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }

        public string Role_Name { get; set; }

        


    }
}
