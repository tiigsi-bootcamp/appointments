namespace Models;

public class Booking
{
	public int Id { get; set; }

	public int UserId { get; set; }
	public User User { get; set; } = new();

	public DateTime AppointmentTime { get; set; }

	public int TimeSlotId { get; set; }
	public TimeSlot TimeSlot { get; set; } = new();

	public decimal PaidAmount { get; set; }

	public decimal Commission { get; set; }

	public decimal DoctorRevenue { get; set; }

	public string TransactionId { get; set; } = "";

	public string PaymentMethod { get; set; } = "";

	public bool IsCompleted { get; set; }

	public DateTime CreatedAt { get; set; }
}
