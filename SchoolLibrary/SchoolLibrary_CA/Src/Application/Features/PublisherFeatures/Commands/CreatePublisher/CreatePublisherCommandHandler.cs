using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.PublisherFeatures.Commands.CreatePublisher
{
    public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public CreatePublisherCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }

            
        public async Task<Guid> Handle(CreatePublisherCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreatePublisherCommand, Publisher>(command);

            await _context.Publishers.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.PublisherId;
        }
    }
}