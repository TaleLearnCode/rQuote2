namespace TaleLearnCode.rQuote.Entities;

/// <summary>
/// Represents an author of one or more quotes within the database.
/// </summary>
public partial class Author
{
	/// <summary>
	/// Identifier of the author record using a slug format.
	/// </summary>
	public string AuthorId { get; set; } = null!;

	/// <summary>
	/// The name of the author.
	/// </summary>
	public string AuthorName { get; set; } = null!;

	public string Bio { get; set; } = null!;

	/// <summary>
	/// The UTC date/time the author was added.
	/// </summary>
	public DateTime DateAdded { get; set; }

	/// <summary>
	/// Collection of the author's quotes.
	/// </summary>
	public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();

}