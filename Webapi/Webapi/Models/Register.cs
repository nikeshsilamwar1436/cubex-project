using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Models
{
    public class Register
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Schoolname { get; set; }
        public string Phonenumber { get; set; }
    }
}
