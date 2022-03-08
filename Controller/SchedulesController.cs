using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Data;
using Models;
using ViewModels;

namespace Controllers;

[Route("[controller]")]
[ApiController]
public class SchedulesController : ControllerBase
{
	private readonly AppointmentsDbContext _context;

	public SchedulesController(AppointmentsDbContext context)
	{
		_context = context;
	}
	
	// GET /schedules
	[HttpGet]
	public  async Task<IActionResult> GetMySchedules()
	{
		var doctorId = 4; // TODO: Get the actual doctorId from the session.
		var schedules = await _context.Schedules
			.Include(s => s.TimeSlots)
			.Where(s => s.DoctorId == doctorId)
			.ToListAsync();

		return Ok(schedules);
	}

	// POST /schedules
	[HttpPost]
	public  async Task<IActionResult> Add(ScheduleViewModel viewModel)
	{
		var schedule = new Schedule
		{
			Day = viewModel.Day,
			Location = viewModel.Location,
			CreatedAt = DateTime.UtcNow,
			DoctorId = 4,
			IsAvailable = true
		};

		_context.Schedules.Add(schedule);
		await _context.SaveChangesAsync();

		return Created("", schedule);
	}

	// PUT /schedules/{id}
	[HttpPut("{id}")]
	public  async Task<IActionResult> Update(int id, [FromBody]ModifyScheduleViewModel viewModel)
	{
		var schedule = await _context.Schedules.FindAsync(id);
		if (schedule is null) 
		{
			return BadRequest("Invalid schedule");
		}

		// TODO: Only owner of the schedule can update it.
		var doctorId = 4;
		if (schedule.DoctorId != doctorId)
		{
			return BadRequest("You don't own that schedule.");
		}

		schedule.Location = viewModel.Location;
		schedule.Day = viewModel.Day;
		schedule.IsAvailable = viewModel.IsAvailable;

	   await _context.SaveChangesAsync();

		return NoContent();
	}

	// POST /schedules/{id}/timeslots
	[HttpPost("{id}/timeslots")]
	public  async Task<IActionResult> AddTimeSlot(int id, [FromBody]TimeSlotViewModel viewModel)
	{
		var schedule = await _context.Schedules.FindAsync(id);
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
		await _context.SaveChangesAsync();

		return Created("", timeslot);
	}

	// PUT /schedule/timeslots/5
}
