﻿using CommonLayer.RequestModels;
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
      
        public user Userlogin(login model)
        {
            return repository.Userlogin(model);
        }

        //public user UserResetPassword(string email, ResetPasswordModel model)
        //{
        //    return repository.UserResetPassword(email, model);
        //}
    }
}
