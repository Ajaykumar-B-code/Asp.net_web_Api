using CommonLayer.RequestModels;
using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace ManagerLayer.Services
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository) 
        { 
            this.repository = repository;
        }

        public user UserRegistration(RegisterModel model)
        {
            return repository.UserRegistration(model);
        }
      
        public string Userlogin(login model)
        {
            return repository.Userlogin(model);
        }

        public string ForgetPassword(string email)
        {
            return repository.ForgetPassword(email);
        }
        public string GenerateToken(string email, int id)
        {
            return repository.GenerateToken(email, id);
        }
        public bool UserResetPassword(string email, ResetPasswordModel model)
        {
            return repository.UserResetPassword(email, model);
        }
    }
}
