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

	[Function("Quote")]
	public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData request)
		=> await request.CreateResponseAsync(await QuoteServices.GetQuoteListAsync(_sqlContext), _jsonSerializerOptions);

}