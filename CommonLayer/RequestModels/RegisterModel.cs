using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class RegisterModel
    {
        [RegularExpression("^[A-Z][a-z]*$",ErrorMessage ="invalid firstname")]
        public string Fname { get; set; }
        [RegularExpression("^[A-Z][a-z]*",ErrorMessage ="Ivalid lastname")]
        public string Lname { get; set; }
        [RegularExpression("^[a-z1-9]*@[a-z]*.[a-z]{1,3}*", ErrorMessage ="Ivalid email")]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
