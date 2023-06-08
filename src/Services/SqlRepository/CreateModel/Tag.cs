namespace TaleLearnCode.rQuote.SqlRepository;

internal static partial class CreateModel
{
	internal static void Tag(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Tag>(entity =>
		{
			entity.HasKey(e => e.TagId).HasName("pkcTag");

			entity.ToTable("Tag", tb => tb.HasComment("Represents a tag applied to a quote in order to categorize quotes."));

			entity.Property(e => e.TagId)
					.HasMaxLength(100)
					.HasComment("Identifier of the tag record using a slug format.");
			entity.Property(e => e.DateAdded)
					.HasDefaultValueSql("(getutcdate())")
					.HasComment("The UTC date/time the tag was added.");
			entity.Property(e => e.TagName)
					.HasMaxLength(100)
					.HasComment("The name of the tag.");
		});
	}
}