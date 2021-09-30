using System.ComponentModel.DataAnnotations;

namespace graphql_minimal_api
{
    public record class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? PublishedInYear { get; set; }

        [Required]
        public Author Author { get; set; }
    }
}