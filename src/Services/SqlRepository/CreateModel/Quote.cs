namespace TaleLearnCode.rQuote.SqlRepository;

internal static partial class CreateModel
{
	internal static void Quote(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Quote>(entity =>
		{
			entity.HasKey(e => e.QuoteId).HasName("pkcQuote");

			entity.ToTable("Quote", tb => tb.HasComment("Represents an quote."));

			entity.Property(e => e.QuoteId).HasComment("Identifier of the quote record using an auto-incremented value.");
			entity.Property(e => e.AuthorId)
					.HasMaxLength(100)
					.HasComment("Identifier of the author the quote is attributed to.");
			entity.Property(e => e.Content)
					.HasMaxLength(500)
					.HasComment("The content of the quote.");
			entity.Property(e => e.DateAdded)
					.HasDefaultValueSql("(getutcdate())")
					.HasComment("The UTC date/time the quote was added.");

			entity.HasOne(d => d.Author).WithMany(p => p.Quotes)
					.HasForeignKey(d => d.AuthorId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fkQuote_Author");
		});
	}
}