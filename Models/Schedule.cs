namespace Models;

public class Schedule
{
	public int Id { get; set; }

	public DayOfWeek Day { get; set; }

	public string Location { get; set; } = "";

	public int DoctorId { get; set; }
	public Doctor? Doctor { get; set; }

	public bool IsAvailable { get; set; }

	public List<TimeSlot> TimeSlots { get; set; } = new();

	public DateTime CreatedAt { get; set; }
}
