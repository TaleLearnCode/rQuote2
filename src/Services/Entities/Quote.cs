namespace TaleLearnCode.rQuote.Entities;

/// <summary>
/// Represents an quote.
/// </summary>
public partial class Quote
{

	/// <summary>
	/// Identifier of the quote record using an auto-incremented value.
	/// </summary>
	public int QuoteId { get; set; }

	/// <summary>
	/// Identifier of the author the quote is attributed to.
	/// </summary>
	public string AuthorId { get; set; } = null!;

	/// <summary>
	/// The content of the quote.
	/// </summary>
	public string Content { get; set; } = null!;

	/// <summary>
	/// The UTC date/time the quote was added.
	/// </summary>
	public DateTime DateAdded { get; set; }

	/// <summary>
	/// The <see cref="Author"/> the quote is attributed to.
	/// </summary>
	public virtual Author Author { get; set; } = null!;

	/// <summary>
	/// Collection of <see cref="QuoteTags"/> associated with the quote.
	/// </summary>
	public virtual ICollection<QuoteTag> QuoteTags { get; set; } = new List<QuoteTag>();

}