using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Domain.Interfaces.Services
{
    public interface ITokenValidator
    {
        (bool Valid, DateTime? Expiration, Dictionary<string, string>? Claims, string? Error)
            ValidateToken(string token);
    }
}
