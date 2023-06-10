using System.Runtime.Serialization;

namespace TaleLearnCode.rQuote.Exceptions;

[Serializable]
public class UnableToDeleteDueToRelatedDataException<TBaseObject, TRelatedObject> : Exception where TBaseObject : class
{
	public UnableToDeleteDueToRelatedDataException() : base($"Unable to delete the {typeof(TBaseObject).Name} because there are related {typeof(TRelatedObject).Name} objects.") { }
	public UnableToDeleteDueToRelatedDataException(string message) : base(message) { }
	public UnableToDeleteDueToRelatedDataException(string message, Exception innerException) : base(message, innerException) { }
	protected UnableToDeleteDueToRelatedDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}