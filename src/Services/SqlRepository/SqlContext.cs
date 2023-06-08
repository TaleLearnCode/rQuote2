using TaleLearnCode.rQuote.SqlRepository;

namespace TaleLearnCode.rQuote;

public class SqlContext : DbContext
{

	private readonly string? _connectionString = string.Empty;

	public SqlContext(string connectionString) => _connectionString = connectionString;

	public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

	public virtual DbSet<Author> Authors { get; set; } = null!;
	public virtual DbSet<Quote> Quotes { get; set; } = null!;
	public virtual DbSet<QuoteTag> QuoteTags { get; set; } = null!;
	public virtual DbSet<Tag> Tags { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlServer(_connectionString);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		CreateModel.Author(modelBuilder);
		CreateModel.Quote(modelBuilder);
		CreateModel.QuoteTag(modelBuilder);
		CreateModel.Tag(modelBuilder);
	}

}