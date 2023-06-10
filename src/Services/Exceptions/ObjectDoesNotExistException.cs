using System.Runtime.Serialization;

namespace TaleLearnCode.rQuote.Exceptions;

[Serializable]
public class ObjectDoesNotExistException<T> : Exception where T : class
{
	public ObjectDoesNotExistException() : base($"The {typeof(T).Name} does not exists.") { }
	public ObjectDoesNotExistException(string message) : base(message) { }
	public ObjectDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
	protected ObjectDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}