using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class RegisterModel
    {
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
