using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Helpers;
using Data;
using Models;
using ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SchedulesController : ControllerBase
{
	private readonly AppointmentsDbContext _context;

	public SchedulesController(AppointmentsDbContext context)
	{
		_context = context;
	}

	// GET /schedules
	[HttpGet]
	public async Task<IActionResult> GetMySchedules()
	{
		var schedules = await _context.Schedules
			.Include(s => s.TimeSlots)
			.Where(s => s.Doctor.UserId == User.GetId())
			.ToListAsync(HttpContext.RequestAborted);

		return Ok(schedules);
	}

	// POST /schedules
	[HttpPost]
	public async Task<IActionResult> Add(ScheduleViewModel viewModel)
	{
		var doctor = await _context.Doctors
			.Include(d => d.Schedules.Where(s => s.Day == viewModel.Day))
			.SingleOrDefaultAsync(d => d.UserId == User.GetId(), HttpContext.RequestAborted);
		if (doctor is null)
		{
			return BadRequest("You are not a doctor.");
		}

		if (!doctor.IsVerified)
		{
			return BadRequest("You need to be verified first.");
		}

		if (doctor.Schedules.Any())
		{
			return BadRequest("Schedule has been set already!!");
		}
		
		var schedule = new Schedule
		{
			Day = viewModel.Day,
			Location = viewModel.Location,
			CreatedAt = DateTime.UtcNow,
			DoctorId = doctor.Id,
			IsAvailable = true
		};

		_context.Schedules.Add(schedule);
		await _context.SaveChangesAsync(HttpContext.RequestAborted);

		return Created("", schedule);
	}

	// PUT /schedules/{id}
	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] ModifyScheduleViewModel viewModel)
	{
		var schedule = _context.Schedules.Find(id);
		if (schedule is null)
		{
			return BadRequest("Invalid schedule");
		}

		var doctorId = User.GetDoctorId();
		if (schedule.DoctorId != doctorId)
		{
			return BadRequest("You don't own that schedule.");
		}

		schedule.Location = viewModel.Location;
		schedule.Day = viewModel.Day;
		schedule.IsAvailable = viewModel.IsAvailable;

		_context.SaveChanges();

		return NoContent();
	}

	// POST /schedules/{id}/timeslots
	[HttpPost("{id}/timeslots")]
	public async Task<IActionResult> AddTimeSlot(int id, [FromBody] TimeSlotViewModel viewModel)
	{
		var schedule = await _context.Schedules.FindAsync(new object[]{ id }, HttpContext.RequestAborted);
		if (schedule is null)
		{
			return BadRequest($"Schedule with id {id} cannot be recognized.");
		}

		var timeslot = new TimeSlot
		{
			StartTime = viewModel.StartTime,
			EndTime = viewModel.EndTime,
			Description = viewModel.Description,
			MaxAppointments = viewModel.MaxAppointments,
			ScheduleId = schedule.Id,
			CreatedAt = DateTime.UtcNow
		};

		_context.TimeSlots.Add(timeslot);
		await _context.SaveChangesAsync(HttpContext.RequestAborted);

		return Created("", timeslot);
	}

	// PUT /schedule/timeslots/5
}
