namespace TaleLearnCode.rQuote.Extensions;

internal static class ValidationExtensions
{

	internal static void ValidateRequestField<TRequest>(this TRequest request, string propertyName) where TRequest : class
	{
		object? propertyValue = typeof(TRequest)?.GetProperty(propertyName)?.GetValue(request);
		if (string.IsNullOrWhiteSpace(propertyValue?.ToString()))
			throw new ArgumentException($"The {propertyName} value must be supplied.");
	}


}
