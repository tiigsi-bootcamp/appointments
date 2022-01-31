using Microsoft.AspNetCore.Mvc;

using Data;

namespace Controllers;

[Route("[controller]")]
public class DoctorsController : ControllerBase
{
	private readonly AppointmentsDbContext _context;

	public DoctorsController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// GET /doctors
	[HttpGet]
	public IActionResult GetDoctors()
	{
		var doctors = _context.Doctors.ToList();

		return Ok(doctors);
	}
}
