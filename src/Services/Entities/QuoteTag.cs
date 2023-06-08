namespace TaleLearnCode.rQuote.Entities;

/// <summary>
/// Represents the relationship between a quote and a tag.
/// </summary>
public partial class QuoteTag
{

	/// <summary>
	/// Identifier of the tag/quote association using an auto-incremented value.
	/// </summary>
	public int QuoteTagId { get; set; }

	/// <summary>
	/// Identifier of the associated quote.
	/// </summary>
	public int QuoteId { get; set; }

	/// <summary>
	/// Identifier of the associated tag.
	/// </summary>
	public string TagId { get; set; } = null!;

	/// <summary>
	/// The UTC date/time the tag/quote association was added.
	/// </summary>
	public DateTime DateAdded { get; set; }

	/// <summary>
	/// The associated <see cref="Quote"/>.
	/// </summary>
	public virtual Quote Quote { get; set; } = null!;

	/// <summary>
	/// The associated <see cref="Tag"/>.
	/// </summary>
	public virtual Tag Tag { get; set; } = null!;

}