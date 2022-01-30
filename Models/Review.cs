namespace Models;

public class Review
{
	public int Id { get; set; }

	public Booking Booking { get; set; } = new();

	public int Stars { get; set; }

	public string Remarks { get; set; } = "";

	public DateTime CreatedAt { get; set; }
}
