using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
    public class TokenValidator:ITokenValidator
    {
        private readonly IConfiguration _config;

        public TokenValidator(IConfiguration config)
        {
            _config = config;
        }

        public (bool Valid, DateTime? Expiration, Dictionary<string, string>? Claims, string? Error)
            ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                return (true, jwtToken.ValidTo, claims, null);
            }
            catch (Exception ex)
            {
                return (false, null, null, ex.Message);
            }
        }
    }
}
