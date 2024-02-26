using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class ResetPasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
