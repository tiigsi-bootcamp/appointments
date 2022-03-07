using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Helpers;

public static class PrincipalUtilities
{
	public static int GetId(this ClaimsPrincipal principal)
	{
		var isConversionSuccessful = int.TryParse(principal.FindFirstValue(JwtRegisteredClaimNames.Sub), out var userId);
		if (isConversionSuccessful)
		{
			return userId;
		}

		return 0;
	}
}
