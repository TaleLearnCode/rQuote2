using Microsoft.Data.SqlClient;
using TaleLearnCode.rQuote.SqlRepository;

namespace TaleLearnCode.rQuote;

public class SqlContext : DbContext
{

	public SqlContext() : base() { }

	public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

	public virtual DbSet<Author> Authors { get; set; } = null!;
	public virtual DbSet<Quote> Quotes { get; set; } = null!;
	public virtual DbSet<QuoteTag> QuoteTags { get; set; } = null!;
	public virtual DbSet<Tag> Tags { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		SqlConnection connection = new();
		string? envConString = Environment.GetEnvironmentVariable("SqlConnectionString");
		connection.ConnectionString = envConString ?? "Data Source=Beast;Initial Catalog=rQuote;Integrated Security=True;TrustServerCertificate=True";
		optionsBuilder.UseSqlServer(connection);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		CreateModel.Author(modelBuilder);
		CreateModel.Quote(modelBuilder);
		CreateModel.QuoteTag(modelBuilder);
		CreateModel.Tag(modelBuilder);
	}

}