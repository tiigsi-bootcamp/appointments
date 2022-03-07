using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Data;
using Models;
using ViewModels;

namespace Controllers;

[Route("[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
	private readonly AppointmentsDbContext _context;
	
	public BookingsController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// GET /bookings
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAll()
	{
		var bookings = await _context.Bookings.ToListAsync();

		return Ok(bookings);
	}

	// POST /bookings
	[HttpPost]
	public async Task<IActionResult> Add([FromBody]BookingViewModel viewModel)
	{
		var timeSlot = await _context.TimeSlots
			.Include(ts => ts.Schedule).ThenInclude(s => s.Doctor)
			.SingleOrDefaultAsync(ts => ts.Id == viewModel.TimeSlotId);
		if (timeSlot is null)
		{
			return BadRequest("Selected time-slot could not be recognized.");
		}

		if (viewModel.AppointmentTime < DateTime.Today)
		{
			return BadRequest("The selected appointment-time cannot be in the past! 😁");
		}

		if (viewModel.AppointmentTime.DayOfWeek != timeSlot.Schedule.Day)
		{
			return BadRequest("Doctor is not available at the selected day!");
		}

		if(timeSlot.MaxAppointments < timeSlot.Bookings.Count())
		{
           return BadRequest(" Wuu kaa buuxaa manta!! ");
		}
		
		var ticketPrice = timeSlot.Schedule.Doctor.TicketPrice;
		var rate = 0.02m;
		var commission = ticketPrice * rate;

		// TODO: Add real payment gateway (eDahab, Zaad...).

		var transactionId = new Random().Next(10_000, 999_999);
		var booking = new Booking
		{
			AppointmentTime = new DateTime(viewModel.AppointmentTime.Ticks, DateTimeKind.Utc),
			IsCompleted = false,
			UserId = 5, // TODO: Get the userId from session.
			CreatedAt = DateTime.UtcNow,
			TransactionId = $"{transactionId}",
			PaidAmount = ticketPrice,
			Commission = commission,
			DoctorRevenue = ticketPrice - commission,
			PaymentMethod = viewModel.PaymentMethod,
			TimeSlotId = timeSlot.Id,
		};

		await _context.Bookings.AddAsync(booking);
		await _context.SaveChangesAsync();

		return Created("", booking);
	}
}
