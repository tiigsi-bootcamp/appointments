using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ViewModels;

namespace Controllers;

[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly AppointmentsDbContext _context;
	private readonly PasswordHasher<Models.User> _passwordHasher;

	public AuthController(AppointmentsDbContext context,
		PasswordHasher<Models.User> passwordHasher)
	{
		_context = context;
		_passwordHasher = passwordHasher;
	}

	// For testing to set user passwords manually.
	[HttpPost]
	public async Task<IActionResult> SetPassword([FromBody] LoginViewModel viewModel)
	{
		var user = await _context.Users
			.SingleOrDefaultAsync(u => u.Email == viewModel.Email, HttpContext.RequestAborted);
		if (user is null)
		{
			return BadRequest("Invalid user");
		}

		var hashedPassword = _passwordHasher.HashPassword(user, viewModel.Password);

		user.PasswordHash = hashedPassword;
		await _context.SaveChangesAsync(HttpContext.RequestAborted);

		return Ok(hashedPassword);
	}

	// POST /auth/login
	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
	{
		// TODO: Implement Google login.
		// TODO: Implement password login.

		var user = await _context.Users
			.SingleOrDefaultAsync(u => u.Email == viewModel.Email);
		if (user is null)
		{
			return BadRequest("Invalid login attempt.");
		}

		var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, viewModel.Password);
		if (verificationResult is not PasswordVerificationResult.Success)
		{
			return BadRequest("Invalid login attempt.");
		}

		var now = DateTime.Now;

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new(JwtRegisteredClaimNames.Name, user.FullName),
			new(JwtRegisteredClaimNames.Gender, user.Gender),
			new(JwtRegisteredClaimNames.Email, user.Email),
		};

		var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.UserId == user.Id);
		if (doctor is not null)
		{
			claims.Add(new("doctorId", doctor.Id.ToString()));
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
