namespace TaleLearnCode.rQuote.Responses;

public class QuoteListResponse
{
	public int Count { get; set; }
	public int TotalCount { get; set; }
	public int Page { get; set; }
	public int TotalPages { get; set; }
	public IList<QuoteResponse> Results { get; set; } = new List<QuoteResponse>();
}