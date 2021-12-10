using Blog.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class CreatePostViewModel
    {

        public string? Content { get; set; }

        public IFormFile? Image { get; set; }

        

             
    }
}
