namespace Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<(Guid, bool)> RegisterAsync(string userName, string email, string password);
        Task DeleteUserAsync(Guid UserId);
    }
}