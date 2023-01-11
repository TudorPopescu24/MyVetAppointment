using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using VetExpert.Domain;

namespace VetExpert.API.Helpers
{
	public static class AuthenticationExtension
	{
		public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
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
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				
				return computedHash.SequenceEqual(passwordHash);
			}
		}

		public static string CreateToken(ApplicationUser userCredentials, string userRole, string tokenKey)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, userCredentials.UserName),
				new Claim(ClaimTypes.Role, userRole),
				new Claim(ClaimTypes.NameIdentifier, userCredentials.Id.ToString())
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddMonths(1),
				signingCredentials: credentials
			);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
	}
}
