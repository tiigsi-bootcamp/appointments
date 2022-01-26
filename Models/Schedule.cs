namespace Models;

public class Schedule
{
	public int Id { get; set; }

	public DayOfWeek Day { get; set; }

	public string Location { get; set; } = "";

	public Doctor Doctor { get; set; } = new();

	public bool IsAvailable { get; set; }

	public DateTime CreatedAt { get; set; }
}
