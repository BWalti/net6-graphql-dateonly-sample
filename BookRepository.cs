using Microsoft.EntityFrameworkCore;

namespace graphql_minimal_api
{
    public class BookRepository
    {
        private readonly DemoDbContext context;

        public BookRepository(DemoDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Book> GetAll()
        {
            return this.context.Books.Include(b => b.Author);
        }

        public Task Add(Book book)
        {
            this.context.Books.Add(book);
            return this.context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid guid)
        {
            var p = this.context.Books.FirstOrDefault(p => p.Id == guid);
            if (p == null)
            {
                return false;
            }

            var e = this.context.Books.Remove(p);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<Book?> GetById(Guid guid)
        {
            return await context.Books.FindAsync(guid);
        }

        public async Task<Book?> Update(Book book)
        {
            var id = book.Id;
            var entity = this.context.Books.FirstOrDefault(p => p.Id == id);
            if (entity == null)
            {
                return null;
            }

            entity = entity with
            {
                Title = book.Title
            };

            await context.SaveChangesAsync();
            return entity;
        }
    }
}