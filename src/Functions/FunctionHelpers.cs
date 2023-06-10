namespace TaleLearnCode.rQuote;

internal static class FunctionHelpers
{
	internal static string GetFunctionUrl(string functionRoute)
	{
		string? websiteHostname = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
		if (websiteHostname is not null)
		{
			string protocol = (!websiteHostname.ToUpperInvariant().StartsWith("LOCALHOST")) ? "https" : "http";
			return $"{protocol}://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/{functionRoute}";
		}
		else
			return functionRoute;
	}
}