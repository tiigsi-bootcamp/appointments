using Microsoft.AspNetCore.Mvc;

using Data;
using Models;

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

	// GET /users/5
	[HttpGet("{id}")]
	public IActionResult GetUser(int id)
	{
		var user = _context.Users.Find(id);
		if (user is null)
		{
			return NotFound();
		}

		return Ok(user);
	}

	// POST /users
	[HttpPost]
	public IActionResult Add([FromBody] User user)
	{
		_context.Users.Add(user);
		_context.SaveChanges();

		return Created("", user);
	}
}
