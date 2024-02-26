using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public user UserRegistration(RegisterModel model);
        public string Userlogin(login model);

        public string ForgetPassword(string email);

       // public user UserResetPassword(string email, ResetPasswordModel model);
    }
}
