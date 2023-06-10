using TaleLearnCode.rQuote.Extensions;
using TaleLearnCode.rQuote.Responses;

namespace TaleLearnCode.rQuote;

public static class QuoteServices
{

	public static async Task<QuoteListResponse> GetQuoteListAsync(SqlContext sqlContext)
		=> (await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().ToListAsync()).ToListResponse();

	public static async Task<QuoteResponse?> GetQuoteAsync(SqlContext sqlContext, int quoteId)
		=> (await sqlContext.Quotes.Include(x => x.Author).AsNoTracking().FirstOrDefaultAsync(x => x.QuoteId == quoteId)).ToResponse();

}