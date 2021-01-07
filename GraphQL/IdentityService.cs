using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Eshop.GraphQL;
using Eshop.GraphQL.Data;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace GraphQL
{
    public interface IIdentityService
    {
        Task<string> Authenticate(
            string email, 
            string password, 
            ApplicationDbContext context
        );
    }

    public class IdentityService : IIdentityService
    {
        [UseApplicationDbContext]
        public async Task<string> Authenticate(string email, string password, ApplicationDbContext context)
        {

            var roles = new List<string>();

            // User user = await context.Users.Select(u => u.Email == email)

            int id = -1;

            foreach (var user in context.Users) {
                if (user.Email == email) {
                    
                    var passCheck = BC.Verify(password, user.Password);

                    if (passCheck) {

                        if (user.Id == 4) {
                            
                            roles.Add("admin");
                        } else {

                            roles.Add("logged");
                        }
                        id = user.Id;
                    } 
                    break;
                }
            }


            if (roles.Count > 0)
            {
                // return GenerateAccessToken(email, Guid.NewGuid().ToString(), roles.ToArray());
                return GenerateAccessToken(email, id.ToString(), roles.ToArray());
            }

            throw new AuthenticationException();
        }

        private string GenerateAccessToken(string email, string userId, string[] roles)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("secretsecretsecret"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, email)
            };

            claims = claims.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role))).ToList();

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "issuer",
                "audience",
                claims,
                expires: DateTime.Now.AddDays(90),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}