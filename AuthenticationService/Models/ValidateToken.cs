using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class ValidateToken
    {
        public static bool Validate(IConfiguration configuration, string token)
        {
            var audienceConfig = configuration.GetSection("SecurityTokenParameters");
            var key = audienceConfig["securitykey"];
            var keyByteArray = Encoding.ASCII.GetBytes(key);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            SecurityToken securityToken;
            try
            {
                var claimPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out securityToken);
                return true;
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                Console.Write("The token failed validation");
                return false;
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                Console.Write("The token was not well-formed or was invalid for some other reason");
                return false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }

}

