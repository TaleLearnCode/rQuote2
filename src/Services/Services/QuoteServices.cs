using TaleLearnCode.rQuote.Responses;

namespace TaleLearnCode.rQuote;

public static class QuoteServices
{

	public static async Task<QuoteListResponse> GetQuoteListAsync(SqlContext sqlContext)
		=> BuildQuoteListResponse(await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().ToListAsync());

	private static QuoteListResponse BuildQuoteListResponse(List<Quote> quotes)
	{
		QuoteListResponse response = new()
		{
			Count = quotes.Count,
			TotalCount = quotes.Count,
			Page = 1,
			TotalPages = 1
		};
		foreach (Quote quote in quotes)
			response.Results.Add(new()
			{
				Id = quote.QuoteId,
				Content = quote.Content,
				AuthorId = quote.AuthorId,
				AuthorName = quote.Author.AuthorName,
				DateAdded = quote.DateAdded
			});
		return response;
	}

}