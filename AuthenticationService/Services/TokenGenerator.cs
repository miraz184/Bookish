using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Services
{
    public class TokenGenerator : ITokenGenerator
    {
       
        
            public string JWTToken(string username)
            {
                var userClaims = new[]
                {
              new Claim(JwtRegisteredClaimNames.UniqueName,username),
              new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString())
            };

                var userKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("USTAuthenticationAPIKeyforSecurity"));
                var userCredentials = new SigningCredentials(userKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "AuthenticationService",
                    audience: "BookAPI",
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: userCredentials,
                    claims: userClaims
                    );

                var res = new { token = new JwtSecurityTokenHandler().WriteToken(token) };
                var jsonObj = JsonConvert.SerializeObject(res);
                return jsonObj;
            }
        }
    }

