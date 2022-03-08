using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Controllers;

[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly AppointmentsDbContext _context;

	public AuthController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// POST /auth/login
	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Login(string email)
	{
		// TODO: Implement Google login.
		
		var user = await _context.Users
			.SingleOrDefaultAsync(u => u.Email == email);
		if (user is null)
		{
			return BadRequest("Invalid login attempt");
		}

		var now = DateTime.Now;

		var claims =  new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new("Fullname", user.FullName),
			new("gender", user.Gender),
			new("email", user.Email),
		};

		   var doctor = await _context.Doctors.FirstOrDefaultAsync(u => u.UserId == user.Id);

            if (doctor is not null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, doctor.Id.ToString()));
            }

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