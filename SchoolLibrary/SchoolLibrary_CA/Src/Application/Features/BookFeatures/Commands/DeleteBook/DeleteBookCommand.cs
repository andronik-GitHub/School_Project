using MediatR;

namespace Application.Features.BookFeatures.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}