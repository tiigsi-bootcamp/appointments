namespace Helpers;

public static class StringExtensions
{
	public static bool IsEmpty(this string name)
	{
		return string.IsNullOrEmpty(name);
	}
}
