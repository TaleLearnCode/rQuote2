namespace TaleLearnCode.rQuote.Responses;

public class QuoteResponse
{
	public int Id { get; set; }
	public string Content { get; set; } = null!;
	public IList<string>? Tags { get; set; }
	public string? AuthorId { get; set; }
	public string? AuthorName { get; set; }
	public DateTime DateAdded { get; set; }
}