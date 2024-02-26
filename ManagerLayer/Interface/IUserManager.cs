using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IUserManager
    {
        public user UserRegistration(RegisterModel model);
        public string Userlogin(login model);

        public string ForgetPassword(string email);


      //  public user UserResetPassword(string email, ResetPasswordModel model);
    }
}
