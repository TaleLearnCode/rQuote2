using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TaleLearnCode.rQuote;

public class QuoteFunctions
{

	private readonly ILogger _logger;
	private readonly SqlContext _sqlContext;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public QuoteFunctions(ILoggerFactory loggerFactory, SqlContext sqlContext, JsonSerializerOptions jsonSerializerOptions)
	{
		_logger = loggerFactory.CreateLogger<QuoteFunctions>();
		_sqlContext = sqlContext;
		_jsonSerializerOptions = jsonSerializerOptions;
	}

	[Function("GetQuoteList")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "quotes")] HttpRequestData request)
	{
		_logger.LogInformation("GetQuoteList - Getting list of quotes");
		return await request.CreateResponseAsync(await QuoteServices.GetQuoteListAsync(_sqlContext), _jsonSerializerOptions);
	}

	[Function("GetQuoteById")]
	public async Task<HttpResponseData> GetQuoteByIdAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "quotes/{id}")] HttpRequestData request,
		string id)
	{
		try
		{
			ArgumentNullException.ThrowIfNull(id);
			if (id.ToUpperInvariant() == "RANDOM")
			{
				_logger.LogInformation("GetQuoteById - Getting quote by id {id}", id);
				return await request.CreateResponseAsync(await QuoteServices.GetRandomQuoteAsync(_sqlContext), _jsonSerializerOptions);
			}
			else
			{
				_logger.LogInformation("GetQuoteById - Getting random quote", id);
				if (!int.TryParse(id, out int quoteId))
					throw new ArgumentException("The id value must be numeric.");
				return await request.CreateResponseAsync(await QuoteServices.GetQuoteAsync(_sqlContext, quoteId), _jsonSerializerOptions);
			}
		}
		catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetQuoteByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}