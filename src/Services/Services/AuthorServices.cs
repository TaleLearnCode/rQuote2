using TaleLearnCode.rQuote.Exceptions;
using TaleLearnCode.rQuote.Extensions;
using TaleLearnCode.rQuote.Requests;
using TaleLearnCode.rQuote.Responses;

namespace TaleLearnCode.rQuote;

public static class AuthorServices
{

	public static async Task<AuthorListResponse> GetAuthorListAsync(SqlContext sqlContext)
		=> (await GetListAsync(sqlContext)).ToListResponse();

	public static async Task<AuthorResponse?> GetAuthorAsync(SqlContext sqlContext, string authorId)
		=> (await GetAsync(sqlContext, authorId)).ToResponse();

	public static async Task<string> CreateAuthorAsync(SqlContext sqlContext, AuthorRequest authorRequest)
	{

		authorRequest.ValidateRequestField(nameof(AuthorRequest.Name));
		authorRequest.ValidateRequestField(nameof(AuthorRequest.Bio));

		string authorId = GenerateAuthorId(authorRequest.Name);

		if (await GetAsync(sqlContext, authorId) is not null) throw new ObjectAlreadyExistsException<Author>();

		await sqlContext.Authors.AddAsync(new()
		{
			AuthorId = authorId,
			AuthorName = authorRequest.Name,
			Bio = authorRequest.Bio
		});
		await sqlContext.SaveChangesAsync();

		return authorId;

	}

	public static async Task UpdateAuthorAsync(SqlContext sqlContext, AuthorRequest authorRequest, string authorId)
	{

		authorRequest.ValidateRequestField(nameof(AuthorRequest.Name));
		authorRequest.ValidateRequestField(nameof(AuthorRequest.Bio));

		Author? author = await GetAsync(sqlContext, authorId) ?? throw new ObjectDoesNotExistException<Author>();

		author.AuthorName = authorRequest.Name;
		author.Bio = authorRequest.Bio;
		sqlContext.Authors.Update(author);
		await sqlContext.SaveChangesAsync();

	}

	public static async Task DeleteAuthorAsync(SqlContext sqlContext, string authorId, bool deleteQuotes = false)
	{

		ArgumentNullException.ThrowIfNull(authorId);
		Author? author = await GetAsync(sqlContext, authorId) ?? throw new ObjectDoesNotExistException<Author>();

		if (author.Quotes.Any())
		{
			if (!deleteQuotes) throw new UnableToDeleteDueToRelatedDataException<Author, Quote>();
			sqlContext.Quotes.RemoveRange(author.Quotes);
			await sqlContext.SaveChangesAsync();
		}

		sqlContext.Authors.Remove(author);
		await sqlContext.SaveChangesAsync();

	}

	private static async Task<List<Author>> GetListAsync(SqlContext sqlContext)
		=> await sqlContext.Authors.AsNoTracking().ToListAsync();

	private static async Task<Author?> GetAsync(SqlContext sqlContext, string authorId)
		=> await sqlContext.Authors.Include(x => x.Quotes).AsNoTracking().FirstOrDefaultAsync(x => x.AuthorId == authorId);

	private static string GenerateAuthorId(string authorName)
		=> authorName.Replace(" ", "-").ToLowerInvariant();

}