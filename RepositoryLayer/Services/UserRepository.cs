using Microsoft.AspNetCore.DataProtection;
using CommonLayer.RequestModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly demoContext context;
       // private readonly IDataProtector _dataProtector;
       Encryption bcrypt= new Encryption();
        public UserRepository(demoContext context, IDataProtectionProvider dataProvider)
        {
            this.context= context;
            //this._dataProtector = dataProvider.CreateProtector("Encriptionkey");
        }

  

        public user UserRegistration(RegisterModel model)
        {    
                user entity = new user();
                var user = context.UserTable.FirstOrDefault(x => x.Email == model.Email);
                if (user == null)
                {
                    entity.Fname = model.Fname;
                    entity.Lname = model.Lname;
                    entity.Email = model.Email;
                    string encryptedPassword = bcrypt.HashGenerator(model.Password);
                    entity.Password = encryptedPassword;
                    context.UserTable.Add(entity);
                    context.SaveChanges();
                    return entity;
                }
                    throw new Exception("user Already exist");         
            
        }

        public user Userlogin(login model)
        {
            
                user user = context.UserTable.FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                {
             //   string decryptedPassword = this._dataProtector.Unprotect(user.Password);
                    if (bcrypt.MatchPass(model.Password,user.Password))
                    {
                        return user;
                    }
                      throw new Exception("Incorrect password");
                    
                }
                    throw new Exception("Incorrect email");
                
        }
        
        //public user UserResetPassword(string email,ResetPasswordModel model)
        //{
        //      user user = context.UserTable.FirstOrDefault(x =>x.Email == email);
        //    if (user != null)
        //    {
        //        string decryptoldPassword = this._dataProtector.Unprotect(user.Password);
        //        if (decryptoldPassword== model.oldPassword)
        //        {
        //            string encryptedPassword = this._dataProtector.Protect(model.newPassword);
        //            user.Password = encryptedPassword;
        //            context.SaveChanges();
        //            return user;
        //        }
        //        throw new Exception("old password did not match");
        //    }
        //   throw new Exception("user not found!");
        //}
       
    }
}
