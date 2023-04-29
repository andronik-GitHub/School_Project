using MediatR;

namespace Application.Features.AuthorFeatures.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; } = default!;
        public string Nationality { get; set; } = default!;
    }
}