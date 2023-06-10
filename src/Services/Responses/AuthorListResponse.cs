namespace TaleLearnCode.rQuote.Responses;

public class AuthorListResponse
{
	public int Count { get; set; }
	public int TotalCount { get; set; }
	public int Page { get; set; }
	public int TotalPages { get; set; }
	public IList<AuthorResponse> Results { get; set; } = new List<AuthorResponse>();
}