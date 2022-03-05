using Microsoft.AspNetCore.Mvc;

using Data;
using Models;
using Microsoft.EntityFrameworkCore;

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
	public  async Task<IActionResult> Get()
	{
		var users = await _context.Users.OrderByDescending(u => u.FullName).ToListAsync();
		return Ok(users);
	}

	// GET /users/5
	[HttpGet("{id}")]
	public  async Task<IActionResult> GetUser(int id)
	{
		var user = await _context.Users.FindAsync(id);
		if (user is null)
		{
			return NotFound();
		}

		return Ok(user);
	}

	// POST /users
	[HttpPost]
	public  async Task<IActionResult> Add([FromBody] User user)
	{
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return Created("", user);
	}

	// PUT /users/5
	[HttpPut("{id}")]
	public  async Task<IActionResult> Update(int id, [FromBody]User user)
	{
		var targetUser = await _context.Users.FindAsync(id);
		if (targetUser is null)
		{
			return BadRequest();
		}

		targetUser.FullName = user.FullName;
		targetUser.Address = user.Address;
		targetUser.Email = user.Email;
		targetUser.Gender = user.Gender;
		
		_context.Users.Update(targetUser);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	// DELETE /users/5
	[HttpDelete("{id}")]
	public  async Task<IActionResult> Delete(int id)
	{
		var user = await _context.Users.FindAsync(id);
		if (user is null)
		{
			return BadRequest();
		}

		_context.Users.Remove(user);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
