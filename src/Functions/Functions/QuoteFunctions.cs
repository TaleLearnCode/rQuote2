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
	public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData request)
	{
		_logger.LogInformation("GetQuoteList - Getting list of quotes");
		return await request.CreateResponseAsync(await QuoteServices.GetQuoteListAsync(_sqlContext), _jsonSerializerOptions);
	}

	[Function("GetQuoteById")]
	public async Task<HttpResponseData> GetQuoteByIdAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "quotes/{id}")] HttpRequestData request,
		string id)
	{
		_logger.LogInformation("GetQuoteById - Getting quote by id {id}", id);
		return await request.CreateResponseAsync(await QuoteServices.GetQuoteAsync(_sqlContext, Convert.ToInt32(id)), _jsonSerializerOptions);
	}

}