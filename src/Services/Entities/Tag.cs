namespace TaleLearnCode.rQuote.Entities;

/// <summary>
/// Represents a tag applied to a quote in order to categorize quotes.
/// </summary>
public partial class Tag
{

	/// <summary>
	/// Identifier of the tag record using a slug format.
	/// </summary>
	public string TagId { get; set; } = null!;

	/// <summary>
	/// The name of the tag.
	/// </summary>
	public string TagName { get; set; } = null!;

	/// <summary>
	/// The UTC date/time the tag was added.
	/// </summary>
	public DateTime DateAdded { get; set; }

	/// <summary>
	/// Collection of <see cref="QuoteTag"/> entities assocaited with the tag.
	/// </summary>
	public virtual ICollection<QuoteTag> QuoteTags { get; set; } = new List<QuoteTag>();

}