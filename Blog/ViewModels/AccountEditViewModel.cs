using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class AccountEditViewModel : IndexViewModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }


        AccountEditViewModel()
        {

        }

        AccountEditViewModel(AccountEditViewModel viewModel)
        {
            this.Surname = viewModel.Surname;
            this.Name = viewModel.Name;
            this.Birthday = viewModel.Birthday;
            this.Password = viewModel.Password;
            this.Email = viewModel.Email;
        }
    }
}
