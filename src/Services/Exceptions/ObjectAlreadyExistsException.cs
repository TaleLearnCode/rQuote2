using System.Runtime.Serialization;

namespace TaleLearnCode.rQuote.Exceptions;

[Serializable]
public class ObjectAlreadyExistsException<T> : Exception where T : class
{
	public ObjectAlreadyExistsException() : base($"The {typeof(T).Name} already exists.") { }
	public ObjectAlreadyExistsException(string message) : base(message) { }
	public ObjectAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
	public ObjectAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}