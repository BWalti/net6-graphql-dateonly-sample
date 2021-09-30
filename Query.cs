using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors.Definitions;

namespace graphql_minimal_api
{

    public class Query
    {
        public DateOnly GetDate(DateOnly date) => date;

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> Books([Service] BookRepository _bookRepository) => _bookRepository.GetAll();

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Author> Authors([Service] AuthorRepository _bookRepository) => _bookRepository.GetAll();

        public async Task<Author> Author([Service] AuthorRepository _bookRepository, Guid guid)
        {
            var author = await _bookRepository.GetById(guid);
            if (author == null)
            {
                throw new AuthorNotFoundException() { Id = guid };
            }

            return author;
        }
    }
}