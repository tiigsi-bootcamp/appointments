using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Data;
using Models;
using ViewModels;

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
	public async Task <IActionResult> GetAll()
	{
		// TODO: Add pagination and filtering.
		var doctors = await _context.Doctors.Include(d => d.User).ToListAsync();

		return Ok(doctors);
	}

	// GET /doctors/5
	[HttpGet("{id}", Name = nameof(GetSingle))]
	public async Task<IActionResult> GetSingle(int id)
	{
		var doctor = await _context.Doctors.Include(d => d.User)
			.SingleOrDefaultAsync(d => d.Id == id);
		if (doctor is null)
		{
			return NotFound(new
			{
				message = "The requested doctor could not be found!"
			});
		}

		return Ok(doctor);
	}

	// POST /doctors
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] DoctorViewModel doctorViewModel) // Over-posting attack.
	{
		var doctor =  new Doctor
		{
			Phone = doctorViewModel.Phone,
			Specialty = doctorViewModel.Specialty,
			Bio = doctorViewModel.Bio,
			Certificate = doctorViewModel.Certificate,
			CreatedAt = DateTime.UtcNow,
			Picture = doctorViewModel.Picture,
			TicketPrice = doctorViewModel.TicketPrice,
			UserId = 3 // TODO: Use the currently logged in users' id.
		};

		_context.Doctors.Add(doctor);
	    await _context.SaveChangesAsync();

		return Created(nameof(GetSingle), doctor);
	}
}
