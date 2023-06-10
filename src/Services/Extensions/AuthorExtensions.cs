using TaleLearnCode.rQuote.Responses;

namespace TaleLearnCode.rQuote.Extensions;

internal static class AuthorExtensions
{

	internal static AuthorListResponse ToListResponse(this List<Author> authors)
	{
		AuthorListResponse response = new()
		{
			Count = authors.Count,
			TotalCount = authors.Count,
			Page = 1,
			TotalPages = 1
		};
		foreach (Author author in authors)
		{
			AuthorResponse? authorResponse = ToResponse(author);
			if (authorResponse is not null) response.Results.Add(authorResponse);
		}
		return response;
	}

	internal static AuthorResponse? ToResponse(this Author? author)
	{
		if (author is not null)
		{
			AuthorResponse response = new()
			{
				Id = author.AuthorId,
				Name = author.AuthorName,
				Bio = author.Bio,
				DateAdded = author.DateAdded.ToShortDateString(),
			};
			if (author.Quotes.Any())
			{
				response.Quotes = new List<QuoteResponse>();
				foreach (Quote quote in author.Quotes)
				{
					QuoteResponse? quoteResponse = quote.ToResponse();
					if (quoteResponse is not null) response.Quotes.Add(quoteResponse);
				}
			}
			return response;
		}
		return null;
	}

}