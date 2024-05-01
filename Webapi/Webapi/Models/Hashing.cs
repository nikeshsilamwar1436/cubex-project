using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi.Controllers
{
    public class Hashing
    {      
        public static string HashSHA(string password)
        {
            var sha = System.Security.Cryptography.SHA512.Create();
            var inputBytes = Encoding.ASCII.GetBytes(password);
            var hash = sha.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
