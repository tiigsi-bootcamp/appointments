using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Data;
using ViewModels;

namespace Controllers;

[Route("[controller]")]
public class BookingsController : ControllerBase
{
	private readonly AppointmentsDbContext _context;
	
	public BookingsController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// GET /bookings
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var bookings = await _context.Bookings.ToListAsync();

		return Ok(bookings);
	}

	// POST /bookings
	public async Task<IActionResult> Add([FromBody]BookingViewModel viewModel)
	{
		
	}
}
