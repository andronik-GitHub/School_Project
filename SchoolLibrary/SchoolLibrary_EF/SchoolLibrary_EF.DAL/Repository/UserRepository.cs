using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;
        private readonly ISortHelper<User> _sortHelper;
        private readonly IDataShaper<User> _dataShaper;
        
        private readonly IQueryable<User> entities;

        public UserRepository(
            ISortHelper<User> sortHelper,
            IDataShaper<User> dataShaper, 
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;

            entities = _userManager.Users;
        }

        public async Task<Guid> CreateAsync(User entity)
        {
            var result = await _userManager.CreateAsync(entity);
            
            if (result.Succeeded) return entity.Id;
            throw new Exception("Adding user to database failed.");
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
            var result = await _userManager.UpdateAsync(entity);
            
            if (!result.Succeeded) throw new Exception("Error updating user in database.");
        }
        public async Task DeleteAsync(Guid key)
        {
            var entity = await GetByIdAsync(key);
            if (entity != null) await _userManager.DeleteAsync(entity);
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
    }
}
