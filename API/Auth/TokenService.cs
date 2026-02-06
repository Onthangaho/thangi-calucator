using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace API.Auth
{
    //important code

    // This class is responsible for generating JWT tokens for authenticated users
    public class TokenService
    {
        // The IConfiguration interface is used to access the application's configuration settings, such as the JWT secret key and other token-related settings
        private readonly IConfiguration _configuration;
         
         /* The constructor takes an IConfiguration instance as a parameter and assigns it to the _configuration field, 
         allowing the TokenService to access the necessary configuration settings for token generation*/
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method generates a JWT token for the specified user and their associated roles
        public string GenerateToken(ApplicationUser user,IList<string> roles)
        {
            // Create a list of claims that will be included in the JWT token, such as the user's name and their roles
           var claims =new List<Claim>
           {
               
               new Claim(ClaimTypes.Name, user.UserName)
           };

           // Add claims for each role that the user belongs to, allowing the token to carry information about the user's permissions and access levels
           claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));
           // Create signing credentials using the secret key and the HMAC-SHA256 algorithm, which will be used to sign the JWT token and ensure its integrity and authenticity
           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           // Create a new JWT token with the specified issuer, audience, claims, expiration time, and signing credentials 
           var token = new JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
           );
           // Serialize the JWT token to a string and return it, allowing the client to use this token for authentication and authorization in subsequent requests
           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}