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
	public IActionResult GetMySchedules()
	{
		var doctorId = 4; // TODO: Get the actual doctorId from the session.
		var schedules = _context.Schedules
			.Include(s => s.TimeSlots)
			.Where(s => s.DoctorId == doctorId)
			.ToList();

		return Ok(schedules);
	}

	// POST /schedules
	[HttpPost]
	public IActionResult Add(ScheduleViewModel viewModel)
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
		_context.SaveChanges();

		return Created("", schedule);
	}

	// POST /schedules/{id}/timeslots
	[HttpPost("{id}/timeslots")]
	public IActionResult AddTimeSlot(int id, [FromBody]TimeSlotViewModel viewModel)
	{
		var schedule = _context.Schedules.Find(id);
		if (schedule is null)
		{
			return BadRequest($"Schedule with id {id} cannot be recognized.");
		}

		var timeslot = new TimeSlot
		{
			StartTime = TimeOnly.FromTimeSpan(viewModel.StartTime),
			EndTime = TimeOnly.FromTimeSpan(viewModel.EndTime),
			Description = viewModel.Description,
			MaxAppointments = viewModel.MaxAppointments,
			ScheduleId = schedule.Id,
			CreatedAt = DateTime.UtcNow
		};

		_context.TimeSlots.Add(timeslot);
		_context.SaveChanges();

		return Created("", timeslot);
	}
}
