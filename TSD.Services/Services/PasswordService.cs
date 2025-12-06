using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            // Generate salt automatically and hash
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Returns true if password matches the hashed value
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
