using SchoolLibrary_EF.BLL.DTOs.ReviewDTOs;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IReviewService : IGenericService<Guid, GetDTO_Review, InsertDTO_Review, UpdateDTO_Review>
    {
    }
}
