using Application.Features.PublisherFeatures.Commands.CreatePublisher;
using Application.Features.PublisherFeatures.Commands.UpdatePublisher;
using Application.Features.PublisherFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterPublisherConfig
    {
        public static void Registration()
        {
            RegisterCreatePublisherCommandConfig();
            RegisterUpdatePublisherCommandConfig();
            RegisterPublisherDTOConfig();
        }

        private static void RegisterCreatePublisherCommandConfig()
        {
            TypeAdapterConfig<CreatePublisherCommand, Publisher>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Location, src => src.Location)
                .TwoWays();
        }
        private static void RegisterUpdatePublisherCommandConfig()
        {
            TypeAdapterConfig<UpdatePublisherCommand, Publisher>
                .NewConfig()
                .Map(dest => dest.PublisherId, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Location, src => src.Location)
                .TwoWays();
        }
        private static void RegisterPublisherDTOConfig()
        {
            TypeAdapterConfig<PublisherDTO, Publisher>
                .NewConfig()
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Location, src => src.Location)
                .TwoWays();
        }
    }
}