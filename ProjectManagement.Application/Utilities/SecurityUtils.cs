using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Utilities
{
    public class SecurityUtils
    {
        public static string GenerateHash(string str)
        {
            var hash = "";

            // SHA256 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                // Get the hashed string.  
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            return hash;
        }
    }
}
