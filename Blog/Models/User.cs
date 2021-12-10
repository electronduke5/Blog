using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public partial class User
    {
        [NotMapped]
        public string FullName => $"{this.Surname} {this.Name}";

        //[NotMapped]
        //public int Age
        //{
        //    get
        //    {
        //        var today = DateTime.Today;
        //        var age = today.Year - this.BirthDate.Value.Year;
        //        if (BirthDate.Value.Date > today.AddYears(-age))
        //            --age;
        //        return age;
        //    }
        //}
    }
}
