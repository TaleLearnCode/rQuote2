using Microsoft.Azure.Functions.Worker.Http;
using System.Collections.Specialized;
using System.Web;

namespace TaleLearnCode.rQuote.Extensions;

internal static class HttpRequestDataExtensions
{
	internal static bool GetBooleanQueryStringValue(this HttpRequestData httpRequestData, string key, bool defaultValue)
	{
		string? queryStringValue = httpRequestData.GetQueryStringValue(key);
		if (queryStringValue is not null)
		{
			return queryStringValue.ToUpperInvariant() switch
			{
				"TRUE" => true,
				"FALSE" => false,
				_ => defaultValue
			};
		}
		return defaultValue;
	}

	internal static string? GetQueryStringValue(this HttpRequestData httpRequestData, string key)
	{
		if (httpRequestData.TryGetQueryStringValue(key, out string? value))
			return value;
		return null;
	}

	internal static bool TryGetQueryStringValue(this HttpRequestData httpRequestData, string key, out string? value)
	{
		value = null;
		key = key.ToUpperInvariant();
		if (httpRequestData.TryGetQueryStringValues(out Dictionary<string, string>? queryStringValues) && queryStringValues.ContainsKey(key))
		{
			value = queryStringValues[key];
		}
		return value is not null;
	}

	internal static bool TryGetQueryStringValues(this HttpRequestData httpRequestData, out Dictionary<string, string> queryStringValues)
	{
		queryStringValues = httpRequestData.GetQueryStringValues();
		return queryStringValues is not null;
	}

	internal static Dictionary<string, string> GetQueryStringValues(this HttpRequestData httpRequestData)
	{
		if (!string.IsNullOrWhiteSpace(httpRequestData.Url.Query))
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(httpRequestData.Url.Query);
			if (nameValueCollection is not null && nameValueCollection.Count >= 1)
			{
				Dictionary<string, string> dictionary = new();
				foreach (string key in nameValueCollection.Keys)
				{
					string? something = nameValueCollection[key];
					if (!string.IsNullOrEmpty(something))
					{
						dictionary.TryAdd(key.ToUpperInvariant(), something);
					}
				}
				return dictionary;
			}
		}
		return null;
	}
}
