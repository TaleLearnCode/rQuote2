using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TaleLearnCode.rQuote.Entities;
using TaleLearnCode.rQuote.Exceptions;
using TaleLearnCode.rQuote.Requests;

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
				return await request.CreateResponseAsync(await QuoteServices.GetQuoteAsync(_sqlContext, GetQuoteId(id)), _jsonSerializerOptions);
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

	[Function("CreateQuote")]
	public async Task<HttpResponseData> CreateQuoteAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "quotes")] HttpRequestData request)
	{
		try
		{
			QuoteRequest quoteRequest = await request.GetRequestParametersAsync<QuoteRequest>(_jsonSerializerOptions);
			string newQuoteId = await QuoteServices.CreateQuoteAsync(_sqlContext, quoteRequest);
			string response = FunctionHelpers.GetFunctionUrl($"quotes/{newQuoteId}");
			return request.CreateCreatedResponse(response);
		}
		catch (Exception ex) when (ex is HttpRequestDataException || ex is ObjectAlreadyExistsException<Quote>)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex) when (ex is DbUpdateException)
		{
			_logger.LogError("{FunctionName} - Entity Framework Exception: {ExceptionMessage}", nameof(CreateQuoteAsync), ex.InnerException?.Message ?? ex.Message);
			return request.CreateBadRequestResponse(ex.InnerException ?? ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetQuoteByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("UpdateQuote")]
	public async Task<HttpResponseData> UpdateQuoteAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "quotes/{id}")] HttpRequestData request,
		string id)
	{
		try
		{
			QuoteRequest quoteRequest = await request.GetRequestParametersAsync<QuoteRequest>(_jsonSerializerOptions);
			await QuoteServices.UpdateQuoteAsync(_sqlContext, quoteRequest, GetQuoteId(id));
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException || ex is HttpRequestDataException || ex is ObjectDoesNotExistException<Quote>)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex) when (ex is DbUpdateException)
		{
			_logger.LogError("{FunctionName} - Entity Framework Exception: {ExceptionMessage}", nameof(CreateQuoteAsync), ex.InnerException?.Message ?? ex.Message);
			return request.CreateBadRequestResponse(ex.InnerException ?? ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetQuoteByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}


	[Function("DeleteQuote")]
	public async Task<HttpResponseData> DeleteQuoteAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "quotes/{id}")] HttpRequestData request,
		string id)
	{
		try
		{
			await QuoteServices.DeleteQuoteAsync(_sqlContext, GetQuoteId(id));
			return request.CreateResponse(HttpStatusCode.OK);
		}
		catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex) when (ex is ObjectDoesNotExistException<Quote>)
		{
			return request.CreateResponse(HttpStatusCode.NotFound);
		}
		catch (Exception ex) when (ex is DbUpdateException)
		{
			_logger.LogError("{FunctionName} - Entity Framework Exception: {ExceptionMessage}", nameof(CreateQuoteAsync), ex.InnerException?.Message ?? ex.Message);
			return request.CreateBadRequestResponse(ex.InnerException ?? ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetQuoteByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	private static int GetQuoteId(string id)
	{
		ArgumentNullException.ThrowIfNull(id);
		if (!int.TryParse(id, out int quoteId))
			throw new ArgumentException($"The {nameof(id)} value must be numeric.");
		return quoteId;
	}

}