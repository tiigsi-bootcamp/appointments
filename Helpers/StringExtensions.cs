namespace Helpers;

public static class StringExtensions
{
	public static bool IsEmpty(this string input)
	{
		return string.IsNullOrEmpty(input);
	}
}
