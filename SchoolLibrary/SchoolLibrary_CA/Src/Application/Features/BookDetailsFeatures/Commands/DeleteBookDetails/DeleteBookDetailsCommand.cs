using MediatR;

namespace Application.Features.BookDetailsFeatures.Commands.DeleteBookDetails
{
    public class DeleteBookDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}