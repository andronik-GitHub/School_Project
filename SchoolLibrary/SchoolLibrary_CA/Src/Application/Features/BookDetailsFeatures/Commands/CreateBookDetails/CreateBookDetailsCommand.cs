using MediatR;

namespace Application.Features.BookDetailsFeatures.Commands.CreateBookDetails
{
    public class CreateBookDetailsCommand : IRequest<Guid>
    {
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
    }
}