using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Controllers;

[Route("[controller]")]
public class AuthController : ControllerBase
{
	// POST /auth/login
	[HttpPost("login")]
	public IActionResult Login()
	{
		// TODO: Validate user information.

		var now = DateTime.Now;

		var claims =  new List<Claim>
		{
			new("sub", "1"),
			new("Fullname", "User Full name")
		};

		var keyInput = "random_text_with_at_least_32_chars";

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyInput));

		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		
		var token = new JwtSecurityToken("MyAPI", "MyFrontendApp", claims, now, now.AddDays(2), credentials);

		var handler = new JwtSecurityTokenHandler();
		var jwt = handler.WriteToken(token);

		var result = new
		{
			token = jwt
		};

		return Ok(result);
	}
}