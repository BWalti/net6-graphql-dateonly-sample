using System.Runtime.Serialization;

namespace graphql_minimal_api
{
    [Serializable]
    internal class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException()
        {
        }

        public AuthorNotFoundException(string? message) : base(message)
        {
        }

        public AuthorNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Guid Id { get; set; }
    }
}