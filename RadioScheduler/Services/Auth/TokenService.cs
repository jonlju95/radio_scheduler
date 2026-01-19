using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RadioScheduler.Models.Auth;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace RadioScheduler.Services.Auth;

public class TokenService(IConfiguration config) {

	public string GenerateToken(User user) {
		string secretKey = config["Jwt:Secret"]!;

		SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity([
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
			]),
			Expires = DateTime.UtcNow.AddMinutes(60),
			SigningCredentials = credentials,
			Issuer = config["Jwt:Issuer"],
			Audience = config["Jwt:Audience"],
		};

		JsonWebTokenHandler handler = new JsonWebTokenHandler();

		return handler.CreateToken(tokenDescriptor);
	}
}
