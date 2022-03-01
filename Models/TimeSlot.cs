namespace Models;

public class TimeSlot
{
	public int Id { get; set; }

	public int ScheduleId { get; set; }
	public Schedule Schedule { get; set; }

	public TimeSpan StartTime { get; set; }

	public TimeSpan EndTime { get; set; }

	public string Description { get; set; } = "";

	public int MaxAppointments { get; set; }

	public DateTime CreatedAt { get; set; }
}
