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

	public static int GetDoctorId(this ClaimsPrincipal principal)
    {
        var isConversionSuccessful = int.TryParse(principal.FindFirst("DoctorId").Value, out var docterId);
        if(isConversionSuccessful)
        {
            return docterId;
        }
        return 0;
    }
}
