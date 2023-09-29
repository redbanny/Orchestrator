using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrchestratorAPI.JWT.Http
{
    public static class JwtGenerator
    {
        public static string GenerateJWT(string secretKey, string subject, string issuer)
		{
			var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var tokenHandler = new JwtSecurityTokenHandler();
			var claims = new List<Claim>();

			if (!string.IsNullOrEmpty(subject))
			{
				claims.Add(new Claim(JwtRegisteredClaimNames.Sub, subject));
			}

			var jwtToken = new JwtSecurityToken(
				string.IsNullOrEmpty(issuer) ? null : issuer,
				null,
				claims,
				null,
				DateTime.UtcNow.AddMinutes(10),
				new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256));

			var token = tokenHandler.WriteToken(jwtToken);

			return token;
		}
    }
}