using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Data;
using Models;
using ViewModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Helpers;

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
	public IActionResult GetAll()
	{
		// TODO: Add pagination and filtering.
		var doctors = _context.Doctors.Include(d => d.User).ToList();

		return Ok(doctors);
	}

	// GET /doctors/5
	[HttpGet("{id}", Name = nameof(GetSingle))]
	public IActionResult GetSingle(int id)
	{
		var doctor = _context.Doctors.Include(d => d.User)
			.SingleOrDefault(d => d.Id == id);
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
	public IActionResult Add([FromBody] DoctorViewModel doctorViewModel) // Over-posting attack.
	{
		var doctor = new Doctor
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
		_context.SaveChanges();

		return Created(nameof(GetSingle), doctor);
	}
}
