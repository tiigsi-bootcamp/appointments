using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Google.Apis.Auth;
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

		var result = await CreateToken(user);

		return Ok(result);
	}

	// POST /auth/social-login
	[HttpPost("social-login")]
	[AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] SocialLoginViewModel viewModel)
	{
		if (!viewModel.Provider.Equals("Google", StringComparison.OrdinalIgnoreCase))
		{
			return BadRequest("Selected provider is not supported.");
		}

		if (string.IsNullOrEmpty(viewModel.Token))
		{
			return BadRequest("Invalid login attempt.");
		}

		GoogleJsonWebSignature.Payload? socialUser = null;

		try
		{
			var validationSettings = new GoogleJsonWebSignature.ValidationSettings
			{
				Audience = new string[] { "385352880967-jn9jkdi0a3lheu4oc3q71jhu7bj820eh.apps.googleusercontent.com" }
			};

			socialUser = await GoogleJsonWebSignature.ValidateAsync(viewModel.Token, validationSettings);
		}
		catch
		{
			return BadRequest("Invalid login attempt.");
		}

		if (socialUser is null)
		{
			return BadRequest("Invalid login attempt.");
		}

		var user = await _context.Users
			.SingleOrDefaultAsync(u => u.Email == socialUser.Email, HttpContext.RequestAborted);
		if (user is null)
		{
			user = new Models.User
			{
				FullName = socialUser.Name,
				Email = socialUser.Email,
				CreatedAt = DateTime.UtcNow,
				IsDisabled = false,
				PasswordHash = "",
				Address = "",
				Gender = "N/A"
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync(HttpContext.RequestAborted);

			Console.WriteLine("New user has been created!");
		}

		var result = await CreateToken(user);
		return Ok(result);
	}

	private async Task<object> CreateToken(Models.User user)
	{
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

		return new
		{
			token = jwt
		};
	}
}
