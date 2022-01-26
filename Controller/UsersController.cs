using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase
{
	// GET /users
	[HttpGet]
	public IActionResult Get()
	{
		return Ok("List of users");
	}
}
