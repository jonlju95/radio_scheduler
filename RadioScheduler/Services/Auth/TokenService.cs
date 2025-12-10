using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RadioScheduler.Models.Auth;

namespace RadioScheduler.Services.Auth;

public class TokenService(IConfiguration config) {

	public string GenerateToken(User user) {
		Claim[] claims = [
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username)
		];

		SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
		SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		JwtSecurityToken token = new JwtSecurityToken(
			issuer: config["Jwt:Issuer"],
			audience: config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(15),
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
