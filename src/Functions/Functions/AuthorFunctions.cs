using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TaleLearnCode.rQuote.Entities;
using TaleLearnCode.rQuote.Exceptions;
using TaleLearnCode.rQuote.Extensions;
using TaleLearnCode.rQuote.Requests;

namespace TaleLearnCode.rQuote;

public class AuthorFunctions
{

	private readonly ILogger _logger;
	private readonly SqlContext _sqlContext;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public AuthorFunctions(ILoggerFactory loggerFactory, SqlContext sqlContext, JsonSerializerOptions jsonSerializerOptions)
	{
		_logger = loggerFactory.CreateLogger<AuthorFunctions>();
		_sqlContext = sqlContext;
		_jsonSerializerOptions = jsonSerializerOptions;
	}

	[Function("GetAuthorList")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "authors")] HttpRequestData request)
	{
		_logger.LogInformation("GetAuthorList - Getting list of authors");
		return await request.CreateResponseAsync(await AuthorServices.GetAuthorListAsync(_sqlContext), _jsonSerializerOptions);
	}

	[Function("GetAuthorById")]
	public async Task<HttpResponseData> GetAuthorByIdAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "authors/{id}")] HttpRequestData request,
		string id)
	{
		try
		{
			ArgumentNullException.ThrowIfNull(id);
			_logger.LogInformation("GetAuthorById - Getting random author", id);
			return await request.CreateResponseAsync(await AuthorServices.GetAuthorAsync(_sqlContext, id), _jsonSerializerOptions);
		}
		catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetAuthorByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("CreateAuthor")]
	public async Task<HttpResponseData> CreateAuthorAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "authors")] HttpRequestData request)
	{
		try
		{
			AuthorRequest authorRequest = await request.GetRequestParametersAsync<AuthorRequest>(_jsonSerializerOptions);
			string newAuthorId = await AuthorServices.CreateAuthorAsync(_sqlContext, authorRequest);
			string response = FunctionHelpers.GetFunctionUrl($"authors/{newAuthorId}");
			return request.CreateCreatedResponse(response);
		}
		catch (Exception ex) when (ex is HttpRequestDataException || ex is ObjectAlreadyExistsException<Author>)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex) when (ex is DbUpdateException)
		{
			_logger.LogError("{FunctionName} - Entity Framework Exception: {ExceptionMessage}", nameof(CreateAuthorAsync), ex.InnerException?.Message ?? ex.Message);
			return request.CreateBadRequestResponse(ex.InnerException ?? ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetAuthorByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("UpdateAuthor")]
	public async Task<HttpResponseData> UpdateAuthorAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "authors/{id}")] HttpRequestData request,
		string id)
	{
		try
		{
			ArgumentNullException.ThrowIfNull(nameof(id));
			AuthorRequest authorRequest = await request.GetRequestParametersAsync<AuthorRequest>(_jsonSerializerOptions);
			await AuthorServices.UpdateAuthorAsync(_sqlContext, authorRequest, id);
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException || ex is HttpRequestDataException || ex is ObjectDoesNotExistException<Author>)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex) when (ex is DbUpdateException)
		{
			_logger.LogError("{FunctionName} - Entity Framework Exception: {ExceptionMessage}", nameof(CreateAuthorAsync), ex.InnerException?.Message ?? ex.Message);
			return request.CreateBadRequestResponse(ex.InnerException ?? ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetAuthorByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}


	[Function("DeleteAuthor")]
	public async Task<HttpResponseData> DeleteAuthorAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "authors/{id}")] HttpRequestData request,
		string id)
	{
		try
		{
			await AuthorServices.DeleteAuthorAsync(_sqlContext, id, request.GetBooleanQueryStringValue("DeleteQuotes", false));
			return request.CreateResponse(HttpStatusCode.OK);
		}
		catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException || ex is UnableToDeleteDueToRelatedDataException<Author, Quote>)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex) when (ex is ObjectDoesNotExistException<Author>)
		{
			return request.CreateResponse(HttpStatusCode.NotFound);
		}
		catch (Exception ex) when (ex is DbUpdateException)
		{
			_logger.LogError("{FunctionName} - Entity Framework Exception: {ExceptionMessage}", nameof(CreateAuthorAsync), ex.InnerException?.Message ?? ex.Message);
			return request.CreateBadRequestResponse(ex.InnerException ?? ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("{FunctionName} - Unexpected exception: {ExceptionMessage}", nameof(GetAuthorByIdAsync), ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}