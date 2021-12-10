using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Blog.ViewModels.Validation
{
    

    public class LoginValid : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            string login = (string)value;

            if (Regex.Matches(login, "[а-яА-Я]").Count != 0)
            {
                ErrorMessage = "Логин не должен содержать русских символов.";
                return false;
            }

            if (Regex.Matches(login, "[^a-zA-Z0-9_-]").Count != 0)
            {
                ErrorMessage = "В логине могут присутствовать только латинские символы, цифры, дефисы и нижние подчеркивания";
                return false;
            }

            return true;
        }
    }
}
