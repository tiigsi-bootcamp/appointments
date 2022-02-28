namespace ViewModels;

public class TimeSlotViewModel
{
	public TimeSpan StartTime { get; set; }

	public TimeSpan EndTime { get; set; }

	public string Description { get; set; } = "";

	public int MaxAppointments { get; set; }
}
