using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _uow { get; set; }
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(AuthorDTO entity)
        {
            // We create a Author object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Author author = _mapper.Map<Author>(entity);

            var id = await _uow.Authors.CreateAsync(author);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(await _uow.Authors.GetAllAsync());
        }
        public async Task<AuthorDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            Author? author = await _uow.Authors.GetByIdAsync(id);

            // We create a AuthorDTO object and copy the values ​​of the properties
            // of the author object into its properties (we perform mapping)
            AuthorDTO? authorDTO = _mapper.Map<AuthorDTO?>(author);

            return authorDTO;
        }
        public async Task UpdateAsync(AuthorDTO entity)
        {
            // We create a Author object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Author author = _mapper.Map<Author>(entity);

            await _uow.Authors.UpdateAsync(author);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Authors.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
