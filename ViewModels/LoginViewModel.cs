namespace ViewModels;

public class LoginViewModel
{
	public string Email { get; set; } = "";

	public string Password { get; set; } = "";
}

public class SocialLoginViewModel
{
	public string Provider { get; set; } = "Google";

	public string Token { get; set; } = "";
}
