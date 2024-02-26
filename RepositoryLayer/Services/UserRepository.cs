using Microsoft.AspNetCore.DataProtection;
using CommonLayer.RequestModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly demoContext context;
        // private readonly IDataProtector _dataProtector;
        private readonly IConfiguration config;
        Encryption bcrypt = new Encryption();
        public UserRepository(demoContext context,IConfiguration config)
        {
            this.context = context;
            this.config = config;
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

        public string Userlogin(login model)
        {

            user user = context.UserTable.FirstOrDefault(x => x.Email == model.Email);

            if (user != null)
            {
                //   string decryptedPassword = this._dataProtector.Unprotect(user.Password);
                if (bcrypt.MatchPass(model.Password, user.Password))
                {
                    string token = GenerateToken(user.Email, user.Id);
                    return token;
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
       
        private string GenerateToken(string email, int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("email",email),
                new Claim("UserId",id.ToString())
        };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            var user = context.UserTable.FirstOrDefault(x=>x.Email == email);
            if (user != null)
            {
                var token = GenerateToken(user.Email,user.Id);
                return token;
            }
            else
            {
                return null;
            }
        }
    }

}
    

