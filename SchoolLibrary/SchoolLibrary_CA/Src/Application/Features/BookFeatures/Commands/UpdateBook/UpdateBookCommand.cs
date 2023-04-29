using MediatR;

namespace Application.Features.BookFeatures.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
    }
}