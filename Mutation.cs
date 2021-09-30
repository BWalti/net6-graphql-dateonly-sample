using HotChocolate;

namespace graphql_minimal_api
{
    public class Mutation
    {
        public Author AddAuthor([Service] AuthorRepository _authorRepository, string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName,
                //Birthday = birthday
            };
            _authorRepository.Add(author);
            return author;
        }

        public async Task<Author> UpdateAuthor([Service] AuthorRepository _authorRepository, Guid id, string? firstName, string? lastName)
        {
            var authorToUpdate = await _authorRepository.GetById(id);
            if (authorToUpdate == null)
            {
                throw new AuthorNotFoundException() { Id = id };
            }

            if (firstName != null)
                authorToUpdate.FirstName = firstName;
            if (lastName != null)
                authorToUpdate.LastName = lastName;
            //if (birthday.HasValue)
            //    authorToUpdate.Birthday = birthday.Value;

            await _authorRepository.Update(authorToUpdate);
            return authorToUpdate;
        }

        public async Task<Author> Delete([Service] AuthorRepository _authorRepository, Guid id)
        {
            var authorToDelete = await _authorRepository.GetById(id);
            if (authorToDelete == null)
                throw new AuthorNotFoundException() { Id = id };

            await _authorRepository.Delete(id);
            return authorToDelete;
        }
    }
}
