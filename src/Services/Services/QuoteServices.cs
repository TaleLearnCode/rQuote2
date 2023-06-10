using TaleLearnCode.rQuote.Exceptions;
using TaleLearnCode.rQuote.Extensions;
using TaleLearnCode.rQuote.Requests;
using TaleLearnCode.rQuote.Responses;

namespace TaleLearnCode.rQuote;

public static class QuoteServices
{

	public static async Task<QuoteListResponse> GetQuoteListAsync(SqlContext sqlContext)
		=> (await GetListAsync(sqlContext)).ToListResponse();

	public static async Task<QuoteResponse?> GetQuoteAsync(SqlContext sqlContext, int quoteId)
		=> (await GetAsync(sqlContext, quoteId)).ToResponse();

	public static async Task<QuoteResponse?> GetRandomQuoteAsync(SqlContext sqlContext)
		=> GetRandomAsync(await GetListAsync(sqlContext)).ToResponse();

	public static async Task<string> CreateQuoteAsync(SqlContext sqlContext, QuoteRequest quoteRequest)
	{

		quoteRequest.ValidateRequestField(nameof(QuoteRequest.AuthorId));
		quoteRequest.ValidateRequestField(nameof(QuoteRequest.Content));

		Quote? existingQuote = await sqlContext.Quotes.FirstOrDefaultAsync(x => x.AuthorId == quoteRequest.AuthorId && x.Content == quoteRequest.Content);
		if (existingQuote is not null)
			throw new ObjectAlreadyExistsException<Quote>();

		Quote quote = new()
		{
			AuthorId = quoteRequest.AuthorId,
			Content = quoteRequest.Content,
		};
		await sqlContext.AddAsync(quote);
		await sqlContext.SaveChangesAsync();
		return quote.QuoteId.ToString();

	}

	public static async Task UpdateQuoteAsync(SqlContext sqlContext, QuoteRequest quoteRequest, int quoteId)
	{

		quoteRequest.ValidateRequestField(nameof(QuoteRequest.AuthorId));
		quoteRequest.ValidateRequestField(nameof(QuoteRequest.Content));

		Quote? quote = await GetAsync(sqlContext, quoteId) ?? throw new ObjectDoesNotExistException<Quote>();

		quote.AuthorId = quoteRequest.AuthorId;
		quote.Content = quoteRequest.Content;
		sqlContext.Quotes.Update(quote);
		await sqlContext.SaveChangesAsync();

	}

	public static async Task DeleteQuoteAsync(SqlContext sqlContext, int quoteId)
	{
		ArgumentNullException.ThrowIfNull(quoteId);
		Quote? quote = await GetAsync(sqlContext, quoteId) ?? throw new ObjectDoesNotExistException<Quote>();
		sqlContext.Quotes.Remove(quote);
		await sqlContext.SaveChangesAsync();
	}

	private static async Task<List<Quote>> GetListAsync(SqlContext sqlContext)
		=> await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().ToListAsync();

	private static async Task<Quote?> GetAsync(SqlContext sqlContext, int quoteId)
		=> await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().FirstOrDefaultAsync(x => x.QuoteId == quoteId);

	private static Quote? GetRandomAsync(List<Quote> quoteList)
		=> quoteList[new Random().Next(quoteList.Count)];

}