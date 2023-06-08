namespace TaleLearnCode.rQuote.SqlRepository;

internal static partial class CreateModel
{
	internal static void QuoteTag(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<QuoteTag>(entity =>
		{
			entity.HasKey(e => e.QuoteTagId).HasName("pkcQuoteTag");

			entity.ToTable("QuoteTag", tb => tb.HasComment("Represents the relationship between a quote and a tag."));

			entity.Property(e => e.QuoteTagId).HasComment("Identifier of the tag/quote association using an auto-incremented value.");
			entity.Property(e => e.DateAdded)
					.HasDefaultValueSql("(getutcdate())")
					.HasComment("The UTC date/time the tag/quote association was added.");
			entity.Property(e => e.QuoteId).HasComment("Identifier of the associated quote.");
			entity.Property(e => e.TagId)
					.HasMaxLength(100)
					.HasComment("Identifier of the associated tag.");

			entity.HasOne(d => d.Quote).WithMany(p => p.QuoteTags)
					.HasForeignKey(d => d.QuoteId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fkQuoteTag_Quote");

			entity.HasOne(d => d.Tag).WithMany(p => p.QuoteTags)
					.HasForeignKey(d => d.TagId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fkQuoteTag_Tag");
		});
	}
}