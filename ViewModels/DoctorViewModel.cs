namespace ViewModels;

public class DoctorViewModel
{
	public string Phone { get; set; } = "";

	public string Specialty { get; set; } = "";

	public IFormFile? Picture { get; set; }

	public string Bio { get; set; } = "";

	public IFormFile? Certificate { get; set; }

	public decimal TicketPrice { get; set; }
}
