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
		var users = _context.Users.OrderByDescending(u => u.FullName).ToList();
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

	// PUT /users/5
	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody]User user)
	{
		var targetUser = _context.Users.Find(id);
		if (targetUser is null)
		{
			return BadRequest();
		}

		targetUser.FullName = user.FullName;
		targetUser.Address = user.Address;
		targetUser.Email = user.Email;
		targetUser.Gender = user.Gender;
		
		_context.Users.Update(targetUser);
		_context.SaveChanges();

		return NoContent();
	}

	// DELETE /users/5
	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var user = _context.Users.Find(id);
		if (user is null)
		{
			return BadRequest();
		}

		_context.Users.Remove(user);
		_context.SaveChanges();

		return NoContent();
	}
}
