using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        IUnitOfWork _uow;

        public AuthorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(AuthorDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Authors.CreateAsync(new Author
            {
                AuthorId = entity.AuthorId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Birthdate = entity.Birthdate,
                Nationality = entity.Nationality,
            });
            _uow.Commit();

            return id;
        }
        public async Task<AuthorDTO> GetAsync(Guid id)
        {
            var entity =  await _uow.Authors.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new AuthorDTO
            {
                AuthorId = entity.AuthorId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Birthdate = entity.Birthdate,
                Nationality = entity.Nationality,
            };
        }
        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var list = await _uow.Authors.GetAllAsync();
            var result = new List<AuthorDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new AuthorDTO
            {
                AuthorId = entity.AuthorId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Birthdate = entity.Birthdate,
                Nationality = entity.Nationality,
            }));

            return result;
        }
        public async Task UpdateAsync(AuthorDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Authors.UpdateAsync(new Author
            {
                AuthorId = entity.AuthorId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Birthdate = entity.Birthdate,
                Nationality = entity.Nationality,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Authors.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
