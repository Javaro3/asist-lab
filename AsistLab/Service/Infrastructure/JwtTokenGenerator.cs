using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Domains;
using Common.Options;
using Microsoft.IdentityModel.Tokens;

namespace Service.Infrastructure;

public static class JwtTokenGenerator
{
    public static string GenerateJwtToken(User user, JwtOption jwtOption)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id.ToString())
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(jwtOption.ByteKey), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtOption.Issuer,
            audience: jwtOption.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtOption.ExpireMinutes)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}