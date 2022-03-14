using Microsoft.AspNetCore.Mvc;

using Data;
using Models;
using ViewModels;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[Route("[controller]")]
[Authorize]
public class DoctorsController : ControllerBase
{
	private readonly AppointmentsDbContext _context;

	public DoctorsController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// GET /doctors
	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> GetAll(int page, int size, string phone)
	{
		// skip = page * size;

		var query = _context.Doctors
			.Include(d => d.User)
			.Skip(page * size)
			.Take(size);

		if (!string.IsNullOrEmpty(phone))
		{
			query = query.Where(d => d.Phone == phone);
		}

		var doctors = await query
			.OrderBy(d => d.Id)
			.ToListAsync(HttpContext.RequestAborted);

		return Ok(doctors);
	}

	// GET /doctors/5
	[HttpGet("{id}", Name = nameof(GetSingle))]
	public async Task<IActionResult> GetSingle(int id)
	{
		var doctor = await _context.Doctors.Include(d => d.User)
			.SingleOrDefaultAsync(d => d.Id == id, HttpContext.RequestAborted);
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
		var doctor = await _context.Doctors
			.SingleOrDefaultAsync(d => d.UserId == User.GetId(), HttpContext.RequestAborted);
		if (doctor is not null)
		{
			return BadRequest("You are already a doctor!");
		}

		doctor = new Doctor
		{
			Phone = doctorViewModel.Phone,
			Specialty = doctorViewModel.Specialty,
			Bio = doctorViewModel.Bio,
			Certificate = doctorViewModel.Certificate,
			CreatedAt = DateTime.UtcNow,
			Picture = doctorViewModel.Picture,
			TicketPrice = doctorViewModel.TicketPrice,
			UserId = User.GetId()
		};

		_context.Doctors.Add(doctor);
		await _context.SaveChangesAsync(HttpContext.RequestAborted);

		return Created(nameof(GetSingle), doctor);
	}
}
