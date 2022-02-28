using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class ScheduleViewModel
{
	public DayOfWeek Day { get; set; }

	[Required]
	public string Location { get; set; } = "";
}
