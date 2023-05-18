namespace Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<object> GetUserByIdAsync(Guid UserId);
        Task<IEnumerable<object>> GetUsersAsync();
        Task<Guid> RegisterUserAsync(string userName, string email, string password, string? role);
        Task<Guid> RegisterAdministratorAsync(string userName, string email, string password);
        Task<Guid> RegisterModeratorAsync(string userName, string email, string password);
        Task UpdateUserAsync(object entity);
        Task DeleteUserAsync(Guid UserId);
    }
}