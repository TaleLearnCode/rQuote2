namespace TaleLearnCode.rQuote.SqlRepository;

internal static partial class CreateModel
{
	internal static void Author(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Author>(entity =>
		{
			entity.HasKey(e => e.AuthorId).HasName("pkcAuthor");

			entity.ToTable("Author", tb => tb.HasComment("Represents an author of one or more quotes within the database."));

			entity.Property(e => e.AuthorId)
					.HasMaxLength(100)
					.HasComment("Identifier of the author record using a slug format.");
			entity.Property(e => e.AuthorName)
					.HasMaxLength(100)
					.HasComment("The name of the author.");
			entity.Property(e => e.Bio).HasMaxLength(500);
			entity.Property(e => e.DateAdded)
					.HasDefaultValueSql("(getutcdate())")
					.HasComment("The UTC date/time the author was added.");
		});
	}
}