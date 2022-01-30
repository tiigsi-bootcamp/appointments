using Microsoft.AspNetCore.Mvc;

using Data;

namespace Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly AppointmentsDbContext _context;

	public UsersController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// GET /users
	[HttpGet]
	public IActionResult Get()
	{
		var users = _context.Users.ToList();
		return Ok(users);
	}
}
