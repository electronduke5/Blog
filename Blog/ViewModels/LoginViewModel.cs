using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Обязательное поле!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Обязательное поле!")]
        public string? Password { get; set; }
    }
}
