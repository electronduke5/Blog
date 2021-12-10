using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.ViewModels.Validation;


namespace Blog.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Обязательное поле!")]
        [MinLength(2, ErrorMessage = "Имя слишком короткое!")]
        [MaxLength(18, ErrorMessage = "Имя слишком длинное!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле!")]
        [MinLength(2, ErrorMessage = "Фамилия слишком короткое!")]
        [MaxLength(30, ErrorMessage = "Фамилия слишком длинное!")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Обязательное поле!")]
        [MinLength(3,ErrorMessage ="Логин слишком короткий.")]
        [LoginValid]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Обязательное поле!")]
        [MinLength(3, ErrorMessage = "Пароль слишком короткий.")]
        [PasswordValid]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Обязательное поле!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string? ConfirmPassword { get; set; }

        public DateTime? BirthDate { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Проверьте правильность адреса электронной почты.")]
        public string? Email { get; set; }


        public int? RoleID { get; set; }

    }
}
