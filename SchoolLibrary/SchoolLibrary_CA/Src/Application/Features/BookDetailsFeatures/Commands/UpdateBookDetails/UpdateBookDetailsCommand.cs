using MediatR;

namespace Application.Features.BookDetailsFeatures.Commands.UpdateBookDetails
{
    public class UpdateBookDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
    }
}