using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
   
    
        public class Login
        {
        [Required, Display(Name="Your User Name Please")]
            public string UserName { get; set; }
            [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        }
    
}