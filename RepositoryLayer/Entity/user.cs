using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class user
    {
        [Key]
        public int Id { get; set; }
        public string Fname {  get; set; }
        public string Lname {  get; set; }

        public string Email {  get; set; }

        public string Password { get; set; }

    }
}
