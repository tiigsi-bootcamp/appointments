namespace Models;

public class User
{
	public int Id { get; set; }

	public string FullName { get; set; } = "";

	public string Email { get; set; } = "";

	public string? Address { get; set; }

	public string Gender { get; set; } = "";

	public bool IsDisabled { get; set; }

	public DateTime CreatedAt { get; set; }
}
