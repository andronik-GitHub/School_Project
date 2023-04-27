using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.PublisherFeatures.Commands
{
    public class CreatePublisherCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
        
        public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public CreatePublisherCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }

            
            public async Task<Guid> Handle(CreatePublisherCommand command, CancellationToken cancellationToken)
            {
                var entity = new Publisher
                {
                    PublisherId = Guid.NewGuid(),
                    Name = command.Name,
                    Location = command.Location
                };

                await _context.Publishers.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.PublisherId;
            }
        }
    }
}