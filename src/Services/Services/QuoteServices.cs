using TaleLearnCode.rQuote.Extensions;
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

	private static async Task<List<Quote>> GetListAsync(SqlContext sqlContext)
		=> await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().ToListAsync();

	private static async Task<Quote?> GetAsync(SqlContext sqlContext, int quoteId)
		=> await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().FirstOrDefaultAsync(x => x.QuoteId == quoteId);

	private static Quote? GetRandomAsync(List<Quote> quoteList)
		=> quoteList[new Random().Next(quoteList.Count)];

}