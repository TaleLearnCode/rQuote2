using TaleLearnCode.rQuote.Responses;

namespace TaleLearnCode.rQuote.Extensions;

internal static class QuoteExtensions
{

	internal static QuoteListResponse ToListResponse(this List<Quote> quotes)
	{
		QuoteListResponse response = new()
		{
			Count = quotes.Count,
			TotalCount = quotes.Count,
			Page = 1,
			TotalPages = 1
		};
		foreach (Quote quote in quotes)
		{
			QuoteResponse? quoteResponse = ToResponse(quote);
			if (quoteResponse is not null) response.Results.Add(quoteResponse);
		}
		return response;
	}

	internal static QuoteResponse? ToResponse(this Quote? quote)
	{
		if (quote is not null)
			return new()
			{
				Id = quote.QuoteId,
				Content = quote.Content,
				AuthorId = quote.AuthorId,
				AuthorName = quote.Author.AuthorName,
				DateAdded = quote.DateAdded
			};
		return null;
	}

}