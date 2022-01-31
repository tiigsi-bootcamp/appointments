namespace Models;

public class TimeSlot
{
	public int Id { get; set; }

	public int ScheduleId { get; set; }
	public Schedule Schedule { get; set; } = new();

	public TimeOnly StartTime { get; set; }

	public TimeOnly EndTime { get; set; }

	public string Description { get; set; } = "";

	public int MaxAppointments { get; set; }

	public DateTime CreatedAt { get; set; }
}
