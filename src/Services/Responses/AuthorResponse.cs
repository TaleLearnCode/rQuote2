namespace TaleLearnCode.rQuote.Responses;

public class AuthorResponse
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string Bio { get; set; } = null!;
	public string DateAdded { get; set; } = null!;
	public string? Url { get; set; }
	public IList<QuoteResponse>? Quotes { get; set; }
}