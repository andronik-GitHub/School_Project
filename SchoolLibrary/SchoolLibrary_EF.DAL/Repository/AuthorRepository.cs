using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Pagging.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public async override Task<Guid> CreateAsync(Author author)
        {
            await entities.AddAsync(author);

            return author.AuthorId;
        }
        public async override Task<IEnumerable<Author>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();

            if (parameters is AuthorParameters param)
                return await (await GetByConditionAsync(p =>
                        p.Birthdate.Year >= param.MinYearOfBirth &&
                        p.Birthdate.Year <= param.MaxYearOfBirth
                    ))
                    .OrderBy(a => a.AuthorId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            else
                return await entities
                    .AsNoTracking()
                    .OrderBy(a => a.AuthorId)
                    .Skip((parameters!.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
        }
    }
}
