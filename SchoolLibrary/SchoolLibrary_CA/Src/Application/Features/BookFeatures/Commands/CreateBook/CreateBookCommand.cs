using MediatR;

namespace Application.Features.BookFeatures.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
    }
}