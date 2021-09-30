using Microsoft.EntityFrameworkCore;

namespace graphql_minimal_api
{
    public class AuthorRepository
    {
        private readonly DemoDbContext context;

        public AuthorRepository(DemoDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Author> GetAll()
        {
            return this.context.Authors;//.Include(p => p.Books);
        }

        public Task Add(Author author)
        {
            this.context.Authors.Add(author);
            return this.context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid guid)
        {
            var p = this.context.Authors.FirstOrDefault(p => p.Id == guid);
            if(p == null)
            {
                return false;
            }

            var e = this.context.Authors.Remove(p);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<Author?> GetById(Guid guid)
        {
            return await context.Authors.FindAsync(guid);
        }

        public async Task<Author?> Update(Author customer)
        {
            var id = customer.Id;
            var entity = this.context.Authors.FirstOrDefault(p => p.Id == id);
            if(entity == null)
            {
                return null;
            }

            entity = entity with
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                //Birthday = customer.Birthday
            };
            
            await context.SaveChangesAsync();
            return entity;
        }
    }
}