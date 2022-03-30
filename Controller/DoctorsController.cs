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

	// Dependency Injection (DI).
	// Constructor Injection.
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

	// GET /doctors/specialties
	[HttpGet("specialties")]
	[AllowAnonymous]
	public async Task<IActionResult> GetSpecialties()
	{
		var specialties = await _context.Doctors
			.GroupBy(d => d.Specialty)
			.Select(g => new // Projection
			{  // Anonymous Types.
				Specialty = g.Key,
				Count = g.Count()
			})
			.ToListAsync(HttpContext.RequestAborted);

		return Ok(specialties);
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
	public async Task<IActionResult> Add([FromForm] DoctorViewModel viewModel,
		[FromServices] IWebHostEnvironment environment)
	{
		if (viewModel.Certificate is null || viewModel.Picture is null)
		{
			return BadRequest("Please upload all the required files.");
		}

		var doctor = await _context.Doctors
			.SingleOrDefaultAsync(d => d.UserId == User.GetId(), HttpContext.RequestAborted);
		if (doctor is not null)
		{
			return BadRequest("You are already a doctor!");
		}

		// FileSystem
		// DB
		// ThirdParty (Like AWS S3, Azure Blob Storage) $$$$$$$

		var filesPath = environment.WebRootPath;

		var avatarExtension = Path.GetExtension(viewModel.Picture.FileName);
		var avatarFileName = "avatar-" + User.GetId() + avatarExtension;

		using var stream = new FileStream(Path.Combine(filesPath, avatarFileName), FileMode.Create);
		await viewModel.Picture.CopyToAsync(stream);

		var certificateExtension = Path.GetExtension(viewModel.Certificate.FileName);
		var certificateFileName = "certificate-" + User.GetId() + certificateExtension;

		await using var certificateStream = new FileStream(Path.Combine(filesPath, certificateFileName), FileMode.Create);
		await viewModel.Certificate.CopyToAsync(certificateStream);

		doctor = new Doctor
		{
			Phone = viewModel.Phone,
			Specialty = viewModel.Specialty,
			Bio = viewModel.Bio,
			Certificate = certificateFileName,
			CreatedAt = DateTime.UtcNow,
			Picture = avatarFileName,
			TicketPrice = viewModel.TicketPrice,
			UserId = User.GetId()
		};

		_context.Doctors.Add(doctor);
		await _context.SaveChangesAsync(HttpContext.RequestAborted);

		return Created(nameof(GetSingle), doctor);
	}
}
