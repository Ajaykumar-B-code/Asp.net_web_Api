using System;
using Bc = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class Encryption
    {
        public string HashGenerator(string input)
        {
            if(input == null)
            {
                throw new Exception("Please Enter the Password");
            }

            int salt = new Random().Next(10, 14);

            string SaltGenerator=Bc.GenerateSalt(salt);
            string HashedPassword=Bc.HashPassword(input, SaltGenerator);
            return HashedPassword;
        }

        public bool MatchPass(string userpass,string hashpass)
        {
            try
            {
                return Bc.Verify(userpass, hashpass);
            }
            catch
            {
                return false;
            }
        }
    }
}
