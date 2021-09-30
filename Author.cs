using HotChocolate;
using HotChocolate.Types;
using System.ComponentModel.DataAnnotations;

namespace graphql_minimal_api
{
    public record class Author
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //[GraphQLType(typeof(DateType))]
        public DateOnly Birthday { get; set; }

        public List<Book> Books { get; set; }
    }
}