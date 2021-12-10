using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.ViewModels.Validation
{
    public class PasswordValid : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            string password = (string)value;

            if (Regex.Matches(password, "\\d").Count == 0)
            {
                ErrorMessage = "Пароль должен содержать хотя бы одну цифру";
                return false;
            }

            if (Regex.Matches(password, "[a-z]").Count == 0)
            {
                ErrorMessage = "Пароль должен содержать хотя бы один строчный латинский символ";
                return false;
            }

            if (Regex.Matches(password, "[A-Z]").Count == 0)
            {
                ErrorMessage = "Пароль должен содержать хотя бы один закглавный латинский символ";
                return false;
            }

            return true;
        }
    }
}
