using System.Dynamic;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class UserRepository :  IUserRepository
    {
        private readonly SchoolLibraryContext dbContext;
        private readonly UserManager<User> _userManager;
        private readonly ISortHelper<User> _sortHelper;
        private readonly IDataShaper<User> _dataShaper;
        private readonly IMapper _mapper;
        
        private readonly IQueryable<User> entities;

        public UserRepository(
            SchoolLibraryContext dbContext,
            ISortHelper<User> sortHelper,
            IDataShaper<User> dataShaper, 
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
            _mapper = mapper;

            entities = _userManager.Users;
        }

        public async Task<Guid> CreateAsync(User entity)
        {
            var result = await _userManager.CreateAsync(entity);

            if (result.Succeeded) return entity.Id;
            
            var errorMessages = string.Join("\n", result.Errors.Select(error => error.Description));
            throw new Exception($"Adding user to database failed. \n Errors: {errorMessages}");

        }
        public async Task<PagedList<User>> GetAllAsync(BaseParameters parameters)
        {
            var collection = entities.AsNoTracking();
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy); // sorting

            return await Task.Run(() => // paging
                PagedList<User>.ToPagedList(sortCollection, parameters.PageNumber, parameters.PageSize));
        }
        public async Task<User?> GetByIdAsync(Guid key)
        {
            return await entities
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == key);
        }
        public async Task UpdateAsync(User entity)
        {
            var existingEntity = await GetByIdAsync(entity.Id);

            if (existingEntity == null) 
                throw new Exception($"{nameof(User)} with id: [{entity.Id}] was not found");
            if (existingEntity.DateDeleted != null) 
                throw new Exception($"{nameof(User)} with id: [{entity.Id}] already deleted");
            
            entity.DateUpdated = DateTime.UtcNow;
            
            var result = await _userManager.UpdateAsync(entity);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join("\n", result.Errors.Select(error => error.Description));
                throw new Exception($"Updating user to database failed. \n Errors: {errorMessages}");
            }
        }
        public async Task DeleteAsync(Guid key)
        {
            var entity = await GetByIdAsync(key);
            
            if (entity == null) 
                throw new Exception($"{nameof(User)} with id: [{key}] was not found");
            if (entity.DateDeleted != null) 
                throw new Exception($"{nameof(User)} with id: [{key}] already deleted");
            
            dbContext.Entry(entity).State = EntityState.Modified;
            entity.DateDeleted = DateTime.UtcNow;
            await UpdateAsync(entity);
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            var collection = entities.AsNoTracking();
            var sortCollection = _sortHelper.ApplySort(collection, parameters.OrderBy);
            var shapedCollection = _dataShaper.ShapeData(sortCollection, parameters.Fields ?? "");
            
            return await Task.Run(() =>
                PagedList<ExpandoObject>.ToPagedList(
                    shapedCollection.AsQueryable(),
                    parameters.PageNumber,
                    parameters.PageSize));
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            var entity = await GetByIdAsync(id);
            return entity == null ? 
                null : 
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }

        
        public async Task<IQueryable<User>> GetByConditionAsync(Expression<Func<User, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
        
        /*  Count the number of books issued per user
         
            SELECT U.FirstName, U.LastName, COUNT(L.BookId) AS BooksLoaned 
            FROM Users U
            LEFT JOIN Loans L ON U.UserId = L.UserId
            GROUP BY U.UserId, U.FirstName, U.LastName;
            
         */
        public async Task<PagedList<(string FirstName, string LastName, int BooksLoaned)>> GetNumBooksIssuedToUserAsync
            (UserParameters parameters)
        {
            var collection = await entities
                .AsNoTracking()
                .GroupJoin(
                    dbContext.Loans.AsNoTracking(),
                    u => u.Id,
                    l => l.UserId,
                    (u, l) => new
                    {
                        u.FirstName,
                        u.LastName,
                        BookLoaned = l.Count()
                    })
                .ToListAsync();

            return PagedList<(string FirstName, string LastName, int BookLoaned)>
                .ToPagedList(
                    collection.Select(item => (item.FirstName, item.LastName, item.BookLoaned)).AsQueryable(), 
                    parameters.PageNumber, 
                    parameters.PageSize);
        }
    }
}
