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
        public UserRepository(demoContext context)
        {
            this.context= context;
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
                    entity.Password = model.Password;
                    context.UserTable.Add(entity);
                    context.SaveChanges();
                    return entity;
                }
                else
                {
                    throw new Exception("user Already exist");
                }          
            
        }

        public user Userlogin(login model)
        {
            
                user user = context.UserTable.FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                {
                    if (user.Password == model.Password)
                    {
                        return user;
                    }
                    else
                    {
                        throw new Exception("Incorrect password");
                    }
                }
                else
                {
                    throw new Exception("Incorrect email");
                }
            
          
        }
            
    }
}
