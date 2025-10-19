using Domain.CustomClaims;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Common;

public static class Utilities
{
    public static void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }

    public static string GenerateToken(IConfiguration configuration, User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(UserClaimTypes.LastName, user.LastName),
            new Claim(UserClaimTypes.AgeClaim, user.Age.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
            new Claim(UserClaimTypes.UserBalanceClaim, user.UserBalance.ToString()),
            new Claim(UserClaimTypes.RegistrationDateClaim, user.RegistrationDate.ToString()),
            new Claim(UserClaimTypes.IsVerifiedClaim, user.IsVerified.ToString()),
            // new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
            new Claim(ClaimTypes.Role, user.UserRole.UserRoleName)
        };

        //if (user.PhoneNumber != string.Empty)
        //    claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!));


        SymmetricSecurityKey symmetricSecurityKey = 
            new(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

        SigningCredentials signingCredentials = 
            new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        SecurityTokenDescriptor securityTokenDescriptor = new()
        {
            Expires = DateTime.Now.AddDays(1),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingCredentials,
        };

        JwtSecurityTokenHandler jwtTokenHandler = new();

        SecurityToken securityToken = jwtTokenHandler.CreateToken(securityTokenDescriptor);

        return jwtTokenHandler.WriteToken(securityToken);
    }
}
