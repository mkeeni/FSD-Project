using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mavericks_Bank.Services
{
    public class TokenService : ITokenService
    {
        private readonly string secretKey;
        private readonly SymmetricSecurityKey symmetricSecurityKey;

        public TokenService(IConfiguration configuration)
        {
            secretKey = configuration["SecretKey"];
            symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        public string GenerateToken(LoginValidationDTO loginValidationDTO)
        {
            var credential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,loginValidationDTO.Email),
                new Claim(ClaimTypes.Role,loginValidationDTO.UserType)
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var userToken = tokenHandler.CreateToken(tokenDescription);
            string convertedToken = tokenHandler.WriteToken(userToken);
            return convertedToken;
        }
    }
}
