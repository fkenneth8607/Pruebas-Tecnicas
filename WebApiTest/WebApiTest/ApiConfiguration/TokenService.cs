using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePortalApi.ApiConfiguration
{
    public class TokenService
    {
        internal string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //TODO: Trar desde config
            var token = new JwtSecurityToken("http://localhost:5000/", "http://localhost:5000/", expires: DateTime.Now.AddMinutes(3), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
